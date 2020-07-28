using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RankingUI : MonoBehaviour
{
    [SerializeField] Text[] texts;
    [SerializeField] StartUI startUI = null;

    // Start is called before the first frame update
    List<Tuple<string, string>> list;

    void Start()
    {
        texts[0].text = "0";
        texts[1].text = "0";
        texts[2].text = "0";
        texts[3].text = "0";
        texts[4].text = "0";
        texts[5].text = "0";
        texts[6].text = "0";
        texts[7].text = "0";
        texts[8].text = "0";
        texts[9].text = "0";

        list = new List<Tuple<string, string>>();
        PrintData();
    }

    void PrintData()
    {
        Backend.BMember.CustomLogin("admin", "admin");
        BackendReturnObject UserDataBro = Backend.GameInfo.GetPublicContents("MaxScore");

        if (UserDataBro.IsSuccess())
        {
            int cnt = UserDataBro.GetReturnValuetoJSON()["rows"].Count;
            Debug.Log("DBcnt: " + cnt);
            for (int i = 0; i < cnt; i++)
            {
                string DBID = UserDataBro.GetReturnValuetoJSON()["rows"][i]["ID"]["S"].ToString();
                string DBMaxscore = UserDataBro.GetReturnValuetoJSON()["rows"][i]["MaxScore"]["N"].ToString();

                list.Add(Tuple.Create(DBID, DBMaxscore));
            }

            list.Sort(delegate (Tuple<string, string> x, Tuple<string, string> y)
            {
                if(int.Parse(x.Item2) > int.Parse(y.Item2))
                {
                    return -1;
                }
                else if(int.Parse(x.Item2) < int.Parse(y.Item2))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            int j = 0;
            foreach(Tuple<string, string> data in list)
            {
                texts[j].text = data.Item1;

                j++;
                if(j > 9)
                {
                    break;
                }
            }
        }
        else
        {
            Debug.Log(UserDataBro.GetMessage());
        }

        Backend.BMember.Logout();


    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                this.gameObject.SetActive(false);
                startUI.gameObject.SetActive(true);
            }
        }
    }
}
