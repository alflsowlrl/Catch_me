using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
using System;

public class ResultUI : MonoBehaviour
{
    [SerializeField] Text comboTxt = null;
    [SerializeField] Text perfectTxt = null;
    [SerializeField] Text missTxt = null;
    [SerializeField] Text scoreTxt = null;

    ScoreManager scoreManager;
    StageManager stageManager;
    ChangeScene changeScene;
    GameManager gameManager;
    DataBaseManager dataBaseManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        stageManager = FindObjectOfType<StageManager>();
        changeScene = FindObjectOfType<ChangeScene>();
        gameManager = FindObjectOfType<GameManager>();
        dataBaseManager = FindObjectOfType<DataBaseManager>();
    }

    public void showResult()
    {
        this.gameObject.SetActive(true);
        scoreTxt.text = string.Format("{0:#,##0}", scoreManager.getTotalScore());
        comboTxt.text = string.Format("{0:#,##0}", scoreManager.getTotalCombo());
        perfectTxt.text = string.Format("{0:#,##0}", scoreManager.GetPerfect());
        missTxt.text = string.Format("{0:#,##0}", scoreManager.getMiss());

        dataBaseManager.UpdateScore(gameManager.getMusicName(), scoreManager.getTotalScore());

        Debug.Log("show Result");

    }

    public void goRetry()
    {
        this.gameObject.SetActive(false);
        stageManager.RestartGame();
    }

    public void goMain()
    {
        Debug.Log("go main");
        Destroy(this);
        SceneManager.LoadScene("select");
    }
}
