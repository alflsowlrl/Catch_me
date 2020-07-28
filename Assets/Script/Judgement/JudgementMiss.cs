using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementMiss : MonoBehaviour
{
    BmsLoader bmsLoader;
    // Start is called before the first frame update
    void Start()
    {
        bmsLoader = FindObjectOfType<BmsLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bmsLoader.destroyNote(other.gameObject);
    }

}
