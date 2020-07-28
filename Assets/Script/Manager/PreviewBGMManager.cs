using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBGMManager : MonoBehaviour
{

    public void changeBGM(string musicName)
    {
        if (AudioManager.instance.isBGMPlaying())
        {
            AudioManager.instance.stopBGM();
        }

        Debug.Log("change bgm");
        AudioManager.instance.playBGMWith(musicName);
    }

    public bool isBGMPlaying()
    {
        return AudioManager.instance.isBGMPlaying();
    }
}
