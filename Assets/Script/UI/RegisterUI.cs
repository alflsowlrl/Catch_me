using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RegisterUI : MonoBehaviour
{
    [SerializeField] InputField id = null;
    [SerializeField] InputField pw = null;
    [SerializeField] InputField repw = null;
    [SerializeField] LoginUI loginUI = null;

    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = pw.text;
        string t_repw = repw.text;

        if (t_repw == t_pw)
        {
            BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw, "");
            if (bro.IsSuccess())
            {
                Debug.Log("회원가입 성공");
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("회원가입 실패");
            }
        }
        else
        {
            Debug.Log("비밀번호가 일치하지 않습니다.");
        }

        this.gameObject.SetActive(false);
        loginUI.gameObject.SetActive(true);
    }

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
                loginUI.gameObject.SetActive(true);
            }
        }
    }

}
