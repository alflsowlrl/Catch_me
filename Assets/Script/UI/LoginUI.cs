using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using UnityEngine.SceneManagement;
using LitJson;

public class LoginUI : MonoBehaviour
{
    [SerializeField] InputField id = null;
    [SerializeField] InputField pw = null;
    [SerializeField] RegisterUI registerUI = null;
    [SerializeField] StartUI startUI = null;
    //[SerializeField] Text Debugging ;

    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(InitializeCallback);
    }

    //private void Awake()
    //{
    //    if(Backend.IsInitialized)
    //    {
    //        BackendAsyncClass.BackendAsync(Backend.BMember.GetUserInfo, result =>
    //        {
    //            if(result.IsSuccess())
    //            {
    //                JsonData t_data = result.GetReturnValuetoJSON();
    //                if(t_data["row"].Count > 0)
    //                {
    //                    Debug.Log(t_data["row"]["inDate"].ToString());
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("not logined");
    //            }
    //        });
    //    }
    //    else
    //    {
    //        Debug.Log("server not initialized");
    //    }
    //}

    private void Awake()
    {
        id.text = "";
        pw.text = "";
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

    void InitializeCallback()
    {
        if (Backend.IsInitialized)
        {
            Debug.Log(Backend.Utils.GetServerTime());
        }
        else
        {
            //ShowToast("server connection failed");
            Debug.Log("server connection failed");
        }
    }

    public void BtnRegist()
    {
        this.gameObject.SetActive(false);
        registerUI.gameObject.SetActive(true);
    }
    public void BtnLogin()
    {

        string t_id = id.text;
        string t_pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(t_id, t_pw);

        if (bro.IsSuccess())
        {

            //ShowToast(t_id + "로그인 성공");

            Debug.Log(t_id + " 로그인 성공");
            Destroy(this);
            SceneManager.LoadScene("select");
            PlayerPrefs.SetString("ID", t_id);
            PlayerPrefs.SetString("IS_LOGINED", "true");
        }
        else
        {

            //ShowToast(t_id + "로그인 실패 " + bro.GetMessage());
            
            Debug.Log(t_id + "로그인 실패");
        }
    }

    //void ShowToast(string message)
    //{
    //    Debugging.text = message;
    //}
}
