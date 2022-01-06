using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int balance;

    void Awake()
    {
        balance = score;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBalance()
    {
        return balance;
    }

    public void AddToScore(int addend)
    {
        score += addend;
        balance += addend;
        print("Current score: " + score);
    }

    public void SubtractFromBalance(int subtrahend)
    {
        balance -= subtrahend;
    }
}
