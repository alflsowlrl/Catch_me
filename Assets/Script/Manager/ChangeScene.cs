using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

public class ChangeScene : MonoBehaviour
{
    public static AudioClip clip;
    public void ChangeToMain()
    {
        SceneManager.LoadScene("Start");
    }

    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("1_select_song");
    }

    public void anacne()
    {
        BmsLoader.bmsFileName = "MINO - anacne.bms";
        SceneManager.LoadScene("2_ingame");
    }

    public void bbibbi()
    {
        BmsLoader.bmsFileName = "IU - bbibbi.bms";
        SceneManager.LoadScene("2_ingame");
    }

    public void twinkle()
    {
        BmsLoader.bmsFileName = "Mozzart - Twinkle.bms";
        SceneManager.LoadScene("2_ingame");
    }

    public void ChangeThirdScene()
    {

        Debug.Log("tttttttt");
        SceneManager.LoadScene("2_ingame");
    }

    public void ChangeFourthScene()
    {
        SceneManager.LoadScene("3_game_result");
    }
}
