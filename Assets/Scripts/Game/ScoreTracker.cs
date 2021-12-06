using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int money;

    void Awake()
    {
        score = 0;
        money = score;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddToScore(int addend)
    {
        score += addend;
        money += addend;
        print("Current score: " + score);
    }

    public void SubtractMoney(int subtrahend)
    {
        money -= subtrahend;
    }
}
