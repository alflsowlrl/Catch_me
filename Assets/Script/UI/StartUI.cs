using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;

public class StartUI : MonoBehaviour
{
    [SerializeField] LoginUI loginUI = null;
    [SerializeField] RankingUI rankingUI = null;

    public void LoginBtn()
    {
        this.gameObject.SetActive(false);
        loginUI.gameObject.SetActive(true);
    }

    public void RankingBtn()
    {
        this.gameObject.SetActive(false);
        rankingUI.gameObject.SetActive(true);
    }

    public void StartBtn()
    {
        PlayerPrefs.SetString("IS_LOGINED", "false");
        Destroy(this);
        SceneManager.LoadScene("select");
    }

    public void Start()
    {
        if(!Backend.IsInitialized)
        {
            Backend.Initialize(backendCallback);
        }
    }

    void backendCallback(BackendReturnObject bro)
    {
        if(bro.IsSuccess())
        {
            Debug.Log("초기화 성공");
        }
        else
        {
            Debug.Log("초기화 실패");
        }
    }

    //public void GetGoogleHash()
    //{
    //    string googleHashKey = Backend.Utils.GetGoogleHash();

    //    if(!string.IsNullOrEmpty(googleHashKey))
    //    {
    //        Debug.Log(googleHashKey);
    //        if(input != null)
    //        {
    //            input.text = googleHashKey;
    //        }
    //    }
    //    else
    //    {
    //        input.text = "이런 키가 없군요";
    //    }
    //}
}
