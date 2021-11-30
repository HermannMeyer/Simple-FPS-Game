using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int score;
    public int money;

    void Awake()
    {
        score = 0;
        money = score;
    }

    int GetScore()
    {
        return score;
    }
}
