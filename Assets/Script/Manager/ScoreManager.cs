using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int totalScore = 0;
    private int totalPerfect = 0;
    private int totalCool = 0;
    private int totalGood = 0;
    private int totalBad = 0;
    private int totalMiss = 0;
    private int currentCombo = 0;
    private int totalCombo = 0;

    private int perfectScore = 10;
    private float perfectWeight = 1.0f;
    private float coolWeight = 0.8f;
    private float goodWeight = 0.5f;
    private float badWeight = 0.1f;

    public void CalculateTotalScore()
    {
        float temp = perfectScore * (totalPerfect * perfectWeight
                         + totalCool * coolWeight
                         + totalGood * goodWeight
                         + totalBad * badWeight);
        totalScore = Mathf.RoundToInt(temp);
    }

    public void Awake()
    {
        initialize();
    }

    public void IncreasePerfect()
    {
        totalPerfect++;
        IncreaseCombo();
        CalculateTotalScore();
    }
    public void IncreaseCool()
    {
        totalCool++;
        IncreaseCombo();
        CalculateTotalScore();
    }

    public void IncreaseGood()
    {
        totalGood++;
        IncreaseCombo();
        CalculateTotalScore();
    }

    public void IncreaseBad()
    {
        totalBad++;
        IncreaseCombo();
        CalculateTotalScore();
    }
    public void IncreaseMiss()
    {
        totalMiss++;
        resetCombo();
    }

    public int GetPerfect()
    {
        return totalPerfect;
    }
    public int GetCool()
    {
        return totalCool;
    }

    public int GetGood()
    {
        return totalGood;   
    }

    public int GetBad()
    {
        return totalBad;
    }

    public int getMiss()
    {
        return totalMiss;
    }

    public int getTotalCombo()
    {
        return totalCombo;
    }

    public void IncreaseCombo()
    {
        currentCombo++;
        totalCombo++;
    }

    public void resetCombo()
    {
        currentCombo = 0;
    }

    public int getTotalScore()
    {
        return totalScore;
    }

    public void initialize()
    {
        totalScore = 0;
        totalPerfect = 0;
        totalCool = 0;
        totalGood = 0;
        totalBad = 0;
        totalMiss = 0;
        currentCombo = 0;
        totalCombo = 0;
    }
}
