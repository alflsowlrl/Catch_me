using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Start()
    {
        instance = this;
    }

    public bool isBGMPlaying()
    {
        return bgmPlayer.isPlaying;
    }

    public void playBGMWithDelay(string p_bgmName, float delay)
    {
        bgmPlayer.clip = Resources.Load<AudioClip>("Audio/BGM/" + p_bgmName);
        Invoke("justPlay", delay);
    }
    public void playBGMWith(string p_bgmName)
    {
        bgmPlayer.clip = Resources.Load<AudioClip>("Audio/BGM/" + p_bgmName);
        bgmPlayer.Play();
    }

    public void justPlay()
    {
        if(bgmPlayer.clip != null)
        {
            bgmPlayer.Play();
        }   
    }

    public void stopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int j = 0; j < sfxPlayer.Length; j++)
        {
            if (!sfxPlayer[j].isPlaying)
            {
                sfxPlayer[j].clip = Resources.Load<AudioClip>("Audio/SFX/" + p_sfxName);
                sfxPlayer[j].Play();
                return;
            }
        }
        Debug.Log("모든 sfx player가 재생중");
        return;
    }

    IEnumerator WaitFor(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

}
