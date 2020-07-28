using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ImageMenuUI : MonoBehaviour
{
    public Image curImage;
    public Sprite newImage;
    GameManager gameManager;
    PreviewBGMManager previewBGMManager;
    private string selectedMusic = "Under The Sunshine";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        previewBGMManager = FindObjectOfType<PreviewBGMManager>();
        if(!previewBGMManager.isBGMPlaying())
        {
            previewBGMManager.changeBGM(selectedMusic);
            gameManager.setMusicName(selectedMusic);
        } 
    }

    // Start is called before the first frame update
    public void ChangeImage()
    {
        curImage.sprite = newImage;
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        
        if(ButtonName.Equals("Under_the_sunshine"))
        {
            selectedMusic = "Under The Sunshine";
        }
        else if(ButtonName.Equals("Axion"))
        {
            selectedMusic = "axion";
        }
        else if(ButtonName.Equals("Blueming"))
        {
            selectedMusic = "Blueming";
        }
        else if(ButtonName.Equals("Sugar"))
        {
            selectedMusic = "Sugar";
        }
        else if (ButtonName.Equals("Adda"))
        {
            selectedMusic = "Adda";
        }
        else
        {
            selectedMusic = "this is error"; //TODO: blueming 노래이름 고치기
        }

        previewBGMManager.changeBGM(selectedMusic);
        gameManager.setMusicName(selectedMusic);

        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
