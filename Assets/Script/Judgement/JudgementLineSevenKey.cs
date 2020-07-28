using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementLineSevenKey : MonoBehaviour
{
    [SerializeField] float perfectWidth = 0.5f;
    [SerializeField] float coolWidth = 0.7f;
    [SerializeField] float goodWidth = 1.0f;
    [SerializeField] float badWidth = 1.2f;

    UnityEngine.KeyCode KeyboardKey = KeyCode.F5;

    BmsLoader bmsLoader;
    ScoreManager scoreManager;
    EffectManager effectManager;

    // Start is called before the first frame update
    void Start()
    {
        bmsLoader = FindObjectOfType<BmsLoader>();
        scoreManager = FindObjectOfType<ScoreManager>();
        effectManager = FindObjectOfType<EffectManager>();

        scoreManager.initialize();

        if (this.gameObject.name.CompareTo("JudgementLine1") == 0)
        {
            KeyboardKey = KeyCode.A;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine2") == 0)
        {
            KeyboardKey = KeyCode.S;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine3") == 0)
        {
            KeyboardKey = KeyCode.D;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine4") == 0)
        {
            KeyboardKey = KeyCode.F;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine5") == 0)
        {
            KeyboardKey = KeyCode.G;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine6") == 0)
        {
            KeyboardKey = KeyCode.H;
        }
        else if (this.gameObject.name.CompareTo("JudgementLine7") == 0)
        {
            KeyboardKey = KeyCode.J;
        }

        Debug.Log(this.gameObject.name + KeyboardKey);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyboardKey))
        {
            checkJudgement(other);
            bmsLoader.destroyNote(other.gameObject);
        }
    }

    private void checkJudgement(Collider other)
    {
        float other_z = other.transform.position.z;
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
        else if (this_z - badWidth / 2 < other_z && other_z < this_z + badWidth / 2)
        {
            scoreManager.IncreaseBad();
            Debug.Log(KeyboardKey + "bad");
            effectManager.JudgementEffect(3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Input.GetKey(KeyboardKey))
        {
            scoreManager.resetCombo();
        }
        effectManager.JudgementEffect(4);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
