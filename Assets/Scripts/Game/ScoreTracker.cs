using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] int balance;
    [SerializeField] GameObject scoreDisplayObj;

    TextMeshProUGUI scoreDisplay;

    void Awake()
    {
        scoreDisplay = scoreDisplayObj.GetComponent<TextMeshProUGUI>();
        scoreDisplay.text = "Points: " + balance.ToString();
    }
    public int GetBalance()
    {
        return balance;
    }

    public void AddToScore(int addend)
    {
        balance += addend;
        scoreDisplay.text = "Points: " + balance.ToString();
        print("Current balance: " + balance);
    }

    public void SubtractFromBalance(int subtrahend)
    {
        balance -= subtrahend;
        scoreDisplay.text = "Points: " + balance.ToString();
    }
}
