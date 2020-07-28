using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageManager : MonoBehaviour
{
    [SerializeField] ResultUI resultUI = null;
    private ScoreManager scoreManager = null;
    private BmsLoader bmsLoader = null;
    private GameManager gameManager = null;
    private bool isGameStart = false;
    private bool isBGMStart = false;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        bmsLoader = FindObjectOfType<BmsLoader>();
        gameManager = FindObjectOfType<GameManager>();

        startGame();
    }

    public void initialize()
    {
        isBGMStart = false;
    }

    private void Update()
    {

        if (!isBGMStart && AudioManager.instance.isBGMPlaying())
        {
            isBGMStart = true;
        }
        else if (isBGMStart && !AudioManager.instance.isBGMPlaying())
        {
            isBGMStart = false;
            ShowResult();    
        }

        else if (Input.GetKeyUp(KeyCode.T))
        {
            StopGame();
            ShowResult();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                StopGame();
                ShowResult();
            }
        }
    }

    public void ShowResult()
    {
        if (!resultUI.isActiveAndEnabled)
        {
            resultUI.showResult();
        }
        
        isGameStart = false;
    }

    public void startGame()
    {
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        BmsLoader bmsLoader = FindObjectOfType<BmsLoader>();
        string musicName = gameManager.getMusicName();
        bmsLoader.BmsLoad(musicName);
        Debug.Log("music: " + AudioManager.instance.isBGMPlaying());
        isGameStart = true;
    }

    public void StopGame()
    {
        BmsLoader bmsLoader = FindObjectOfType<BmsLoader>();
        bmsLoader.DestroyAllObject();
        if (AudioManager.instance.isBGMPlaying())
        {
            AudioManager.instance.stopBGM();
        }

        isGameStart = false;
    }

    public void RestartGame()
    {
        scoreManager.initialize();
        string musicName = gameManager.getMusicName();
        bmsLoader.Restart(musicName);

        isGameStart = true;
    }

    public bool checkGameStarted()
    {
        return isGameStart;
    }
}
