using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;
    [SerializeField] Animator[] boxHitAnimator = null;

    public void JudgementEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }

    public void boxHitEffect(int p_num)
    {
        Debug.Log("line: " + p_num);
        boxHitAnimator[p_num].SetTrigger(hit);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
