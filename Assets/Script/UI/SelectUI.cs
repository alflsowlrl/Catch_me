using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

public class SelectUI : MonoBehaviour
{
    public void PlayBtn()
    {
        Destroy(this);
        SceneManager.LoadScene("PlayGameFiveKey");
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Destroy(this);
                if (PlayerPrefs.GetString("IS_LOGINED").Equals("true"))
                {
                    Backend.BMember.Logout();
                }

                SceneManager.LoadScene("Start");
            }
        }
    }
}
