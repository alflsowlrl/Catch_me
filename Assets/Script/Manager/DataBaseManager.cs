using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BackEnd;
using System.Linq;

public class DataBaseManager : MonoBehaviour
{
    private List<Tuple<String, String, String>> MusicScoredata;
    private List<Tuple<String, String, String>> MaxScoredata;

    private int maxScore = -1;
    private string maxScoreIndate = "";
    private string ID = "";

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }


    public List<Tuple<String, String, String>> getMaxScoreData()
    {
        return MaxScoredata;
    }



    void initialize()
   {
        if (!Backend.IsInitialized)
        {
            Backend.Initialize(InitializeCallback);
        }

        ID = PlayerPrefs.GetString("ID");
        MusicScoredata = new List<Tuple<String, String, String>>();
        MaxScoredata = new List<Tuple<String, String, String>>();
        maxScore = -1;
        maxScoreIndate = "";
        loadData();

        Debug.Log("database initialized");
    }

    void InitializeCallback()
    {
        if (Backend.IsInitialized)
        {
            Debug.Log("server connected");
        }
        else
        {
            //ShowToast("server connection failed");
            Debug.Log("server connection failed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadMusicScoreData()
    {
        if(!Backend.IsInitialized || ID.Equals("") || ID == null)
        {
            return;
        }

        BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "ScoreTable", UserDataBro =>
        {
            if (UserDataBro.IsSuccess())
            {
                int cnt = UserDataBro.GetReturnValuetoJSON()["rows"].Count;
                for (int i = 0; i < cnt; i++)
                {
                    string DBmusic = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Music"]["S"].ToString();
                    string DBscore = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Score"]["S"].ToString();
                    string t_Indate = UserDataBro.GetReturnValuetoJSON()["rows"][i]["inDate"]["S"].ToString();

                    MusicScoredata.Add(Tuple.Create(DBmusic, DBscore, t_Indate));
                }
            }
            else
            {
                Debug.Log(UserDataBro.GetMessage());
            }
        });
    }

    public void LoadMaxScoreData()
    {
        if (!Backend.IsInitialized)
        {
            return;
        }

        BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPublicContents, "MaxScore", UserDataBro =>
        {
            if (UserDataBro.IsSuccess())
            {
                int cnt = UserDataBro.GetReturnValuetoJSON()["rows"].Count;
                Debug.Log("DBcnt: " + cnt);
                for(int i = 0; i < cnt; i++)
                {
                    string DBID = UserDataBro.GetReturnValuetoJSON()["rows"][i]["ID"]["S"].ToString();
                    string DBMaxscore = UserDataBro.GetReturnValuetoJSON()["rows"][i]["MaxScore"]["N"].ToString();
                    string DBIndate = UserDataBro.GetReturnValuetoJSON()["rows"][i]["inDate"]["S"].ToString();

                    if(DBID == ID)
                    {
                        maxScore = int.Parse(DBMaxscore);
                        maxScoreIndate = DBIndate;
                    }

                    MaxScoredata.Add(Tuple.Create(DBID, DBMaxscore, DBIndate));
                }
            }
            else
            {
                Debug.Log(UserDataBro.GetMessage());
            }
        });
    }

    private void UpdateMusicScoreData(string musicName, int score)
    {
        if (!Backend.IsInitialized || ID.Equals("") || ID == null)
        {
            return;
        }

        Tuple<String, String, String> result = MusicScoredata.Find(tuple => {
            return tuple.Item1.Equals(musicName);
        });
        if (result != null)
        {
            if (int.Parse(result.Item2) < score)
            {
                string indate = result.Item3;
                Param param = new Param();
                param.Add("Score", score.ToString());
                param.Add("Music", musicName);

                BackendAsyncClass.BackendAsync(Backend.GameInfo.Update, "ScoreTable", indate, param, (t_callback) =>
                {

                });
            }
        }
        else
        {
            Param param = new Param();
            param.Add("Score", score.ToString());
            param.Add("Music", musicName);

            BackendAsyncClass.BackendAsync(Backend.GameInfo.Insert, "ScoreTable", param, (t_callback) =>
            {

            });
        }
    }

    private void UpdateMaxScoreData(int NewMaxScore)
    {
        if (!Backend.IsInitialized || ID.Equals("") || ID == null)
        {
            return;
        }

        Debug.Log("MaxScoreData");
        Debug.Log(ID + ", " + maxScore +", " + maxScoreIndate);
        Debug.Log("-----------------");

        if (maxScoreIndate == "")
        {
            Param MaxData = new Param();
            MaxData.Add("ID", ID);
            MaxData.Add("MaxScore", NewMaxScore);

            BackendAsyncClass.BackendAsync(Backend.GameInfo.Insert, "MaxScore", MaxData, (t_callback) =>
            {
                maxScoreIndate = t_callback.GetInDate();
            });
        }
        else
        {
            if(maxScore < NewMaxScore)
            {
                Param MaxData = new Param();
                MaxData.Add("ID", ID);
                MaxData.Add("MaxScore", NewMaxScore);

                BackendAsyncClass.BackendAsync(Backend.GameInfo.Update, "MaxScore", maxScoreIndate, MaxData, (t_callback) =>
                {
                    maxScoreIndate = t_callback.GetInDate();
                });
            }
        }

        if(maxScore < NewMaxScore)
        {
            maxScore = NewMaxScore;
        }
    }

    public void UpdateScore(string musicName, int score)
    {
        UpdateMusicScoreData(musicName, score);
        UpdateMaxScoreData(score);

        loadData();
    }

    void loadData()
    {
        LoadMusicScoreData();
        LoadMaxScoreData();
    }
}
