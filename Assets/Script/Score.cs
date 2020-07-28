using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System;



public class Score : MonoBehaviour
{
    private const string Format = "{0:#,##0}";
    private List<Tuple<string, string>> MusicScoredata = new List<Tuple<string, string>>();

    [SerializeField] Text[] scores = null;

    // Start is called before the first frame update
    void Start()
    {
        scores[0].text = string.Format(Format, 0);
        scores[1].text = string.Format(Format, 0);
        scores[2].text = string.Format(Format, 0);
        scores[3].text = string.Format(Format, 0);
        scores[4].text = string.Format(Format, 0);

        if (Backend.IsInitialized)
        {
            LoadData();
        }
        else
        {
            Backend.Initialize(InitializeCallback);
            LoadData();
        }
    }

    private void Awake()
    {
        if (Backend.IsInitialized)
        {
            LoadData();
        }
        else
        {
            Backend.Initialize(InitializeCallback);
            LoadData();
        }
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

    void LoadData()
    {
        string isLogined = PlayerPrefs.GetString("IS_LOGINED");
        Debug.Log("isLogined: " + isLogined);
        if (isLogined.Equals("false"))
        {
            return;
        }

        MusicScoredata = new List<Tuple<string, string>>();
        Debug.Log("load data");
        BackendReturnObject UserDataBro =  Backend.GameInfo.GetPrivateContents("ScoreTable");

        
        if (UserDataBro.IsSuccess())
        {
            Debug.Log("db success");
            int cnt = UserDataBro.GetReturnValuetoJSON()["rows"].Count;
            for (int i = 0; i < cnt; i++)
            {
                string DBmusic = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Music"]["S"].ToString();
                string DBscore = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Score"]["S"].ToString();

                if (DBmusic.Equals("Under The Sunshine"))
                {
                    scores[0].text = string.Format(Format, DBscore);
                }
                else if (DBmusic.Equals("axion"))
                {
                    scores[1].text = string.Format(Format, DBscore);
                }
                else if (DBmusic.Equals("Blueming"))
                {
                    scores[2].text = string.Format(Format, DBscore);
                }
                else if (DBmusic.Equals("Sugar"))
                {
                    scores[3].text = string.Format(Format, DBscore);
                }
                else if (DBmusic.Equals("Adda"))
                {
                    scores[4].text = string.Format(Format, DBscore);
                }
            }
        }
        else
        {
            Debug.Log(UserDataBro.GetMessage());
        }

        //BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "ScoreTable", UserDataBro =>
        //{
        //    Debug.Log("db success");
        //    if (UserDataBro.IsSuccess())
        //    {

        //        int cnt = UserDataBro.GetReturnValuetoJSON()["rows"].Count;
        //        for (int i = 0; i < cnt; i++)
        //        {
        //            string DBmusic = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Music"]["S"].ToString();
        //            string DBscore = UserDataBro.GetReturnValuetoJSON()["rows"][i]["Score"]["S"].ToString();

        //            if(DBmusic.Equals("Under_the_sunshine"))
        //            {
        //                scores[0].text = string.Format(Format, DBscore);
        //            }
        //            else if (DBmusic.Equals("Axion"))
        //            {
        //                scores[1].text = string.Format(Format, DBscore);
        //            }
        //            else if (DBmusic.Equals("Blueming"))
        //            {
        //                scores[2].text = string.Format(Format, DBscore);
        //            }
        //            else if (DBmusic.Equals("Sugar"))
        //            {
        //                scores[3].text = string.Format(Format, DBscore);
        //            }
        //            else if (DBmusic.Equals("Adda"))
        //            {
        //                scores[4].text = string.Format(Format, DBscore);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log(UserDataBro.GetMessage());
        //    }
        //});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
