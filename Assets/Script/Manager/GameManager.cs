using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string musicName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMusicName(string musicName)
    {
        PlayerPrefs.SetString("MUSIC_NAME", musicName);
    }

    public string getMusicName()
    {
        return PlayerPrefs.GetString("MUSIC_NAME"); ;
    }

    public float getMusicBias(string musicName)
    {
        if(musicName == "axion")
        {
            return 0.0f;
        }
        else if(musicName == "Under The Sunshine")
        {
            return 10.0f;
        }
        else if (musicName == "Blueming")
        {
            return 7.0f;
        }
        else if (musicName == "Sugar")
        {
            return 10.0f;
        }
        else if (musicName == "Adda")
        {
            return 10.0f;
        }
        else
        {
            return 0.0f;
        }
    }
}
