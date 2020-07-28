using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementLineFiveKey : MonoBehaviour
{
    float perfectWidth = 1.5f;
    float coolWidth = 2.0f;
    float goodWidth = 2.5f;
    float badWidth = 3.0f;
    float note_bias = 1.414f;

    UnityEngine.KeyCode KeyboardKey = KeyCode.F5;

    BmsLoader bmsLoader;
    ScoreManager scoreManager;
    EffectManager effectManager;
    StageManager stageManager;

    private float minX = 0;
    private float maxX = 0;
    private float maxY = 0;

    private int line_num;

    private void Awake()
    {
        initialize();
    }

    public void initialize()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        bmsLoader = FindObjectOfType<BmsLoader>();
        scoreManager = FindObjectOfType<ScoreManager>();
        effectManager = FindObjectOfType<EffectManager>();
        stageManager = FindObjectOfType<StageManager>();

        scoreManager.initialize();

        if (this.gameObject.name.CompareTo("JudgementLine1") == 0)
        {
            minX = 140;
            maxX = 500;
            maxY = 500;
            line_num = 0;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine2") == 0)
        {
            minX = 520;
            maxX = 740;
            maxY = 500;
            line_num = 1;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine3") == 0)
        {
            minX = 800;
            maxX = 1080;
            maxY = 500;
            line_num = 2;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine4") == 0)
        {
            minX = 1100;
            maxX = 1380;
            maxY = 500;
            line_num = 3;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine5") == 0)
        {
            minX = 1400;
            maxX = 1700;
            maxY = 500;
            line_num = 4;
        }

        //if (this.gameObject.name.CompareTo("JudgementLine1") == 0)
        //{
        //    KeyboardKey = KeyCode.A;
        //    line_num = 0;
        //}
        //else if (this.gameObject.name.CompareTo("JudgementLine2") == 0)
        //{
        //    KeyboardKey = KeyCode.S;
        //    line_num = 1;
        //}
        //else if (this.gameObject.name.CompareTo("JudgementLine3") == 0)
        //{
        //    KeyboardKey = KeyCode.D;
        //    line_num = 2;
        //}
        //else if (this.gameObject.name.CompareTo("JudgementLine4") == 0)
        //{
        //    KeyboardKey = KeyCode.F;
        //    line_num = 3;
        //}
        //else if (this.gameObject.name.CompareTo("JudgementLine5") == 0)
        //{
        //    KeyboardKey = KeyCode.G;
        //    line_num = 4;
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        //if (Input.touchCount == 1 &&
        //    Input.GetTouch(0).phase == TouchPhase.Began &&
        //    Input.GetTouch(0).position.x >= minX &&
        //    Input.GetTouch(0).position.x <= maxX &&
        //    Input.GetTouch(0).position.y <= maxY)
        //{
        //    checkJudgement(other);
        //    bmsLoader.destroyNote(other.gameObject);
        //}

        Debug.Log(line_num + " tcnt: " + Input.touchCount);
        for(int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began &&
            Input.GetTouch(i).position.x >= minX &&
            Input.GetTouch(i).position.x <= maxX &&
            Input.GetTouch(i).position.y <= maxY)
            {
                checkJudgement(other);
                bmsLoader.destroyNote(other.gameObject);
                break;
            }
        }


        //if (Input.GetKey(KeyboardKey))
        //{
        //    checkJudgement(other);
        //    bmsLoader.destroyNote(other.gameObject);
        //}
    }

    private void checkJudgement(Collider other)
    {
        float other_z = other.transform.position.z - note_bias;
        float this_z = this.transform.position.z;

        if (this_z - perfectWidth / 2 < other_z && other_z < this_z + perfectWidth / 2)
        {
            scoreManager.IncreasePerfect();
            Debug.Log(KeyboardKey + "perfect");
            effectManager.JudgementEffect(0);
        }
        else if (this_z - coolWidth / 2 < other_z && other_z < this_z + coolWidth / 2)
        {
            scoreManager.IncreaseCool();
            Debug.Log(KeyboardKey + "cool");
            effectManager.JudgementEffect(1);
        }
        else if (this_z - goodWidth / 2 < other_z && other_z < this_z + goodWidth / 2)
        {
            scoreManager.IncreaseGood();
            Debug.Log(KeyboardKey + "good");
            effectManager.JudgementEffect(2);
        }
        else 
        {
            scoreManager.IncreaseBad();
            Debug.Log(KeyboardKey + "bad");
            effectManager.JudgementEffect(3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(Input.touchCount == 0)
        {
            effectManager.JudgementEffect(4);
            scoreManager.IncreaseMiss();
        }
    }



    // Update is called once per frame
    void Update()
    {
        //if (!isBGMStart && AudioManager.instance.isBGMPlaying())
        //{
        //    isBGMStart = true;
        //}
        //if (isBGMStart && !AudioManager.instance.isBGMPlaying())
        //{
        //    isBGMStart = false;

        //    if(stageManager.checkGameStarted())
        //    {
        //        this.gameObject.SetActive(false);
        //        stageManager.ShowResult();
        //    }
        //}

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began &&
            Input.GetTouch(i).position.x >= minX &&
            Input.GetTouch(i).position.x <= maxX &&
            Input.GetTouch(i).position.y <= maxY)
            {
                effectManager.boxHitEffect(line_num);
                break;
            }
        }

    }
}
