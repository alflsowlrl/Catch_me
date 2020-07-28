using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Text[] scores;

    // Start is called before the first frame update
    void Start()
    {
        scores[0].text = "1000";
        scores[1].text = "1000";
        scores[2].text = "1000";
        scores[3].text = "1000";
        scores[4].text = "1000";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
