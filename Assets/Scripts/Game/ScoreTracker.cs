using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] int balance; // The player's current point balance
    [SerializeField] GameObject scoreDisplayObj; // Score display UI game object

    TextMeshProUGUI scoreDisplay; // Score display text

    // Awake is called as the script instance is loaded (before Start).
    void Awake()
    {
        scoreDisplay = scoreDisplayObj.GetComponent<TextMeshProUGUI>();
        scoreDisplay.text = "Points: " + balance.ToString();
    }

    // Return the current point balance of the player.
    public int GetBalance()
    {
        return balance;
    }

    // Add points to the point balance.
    public void AddToBalance(int addend)
    {
        balance += addend;
        scoreDisplay.text = "Points: " + balance.ToString();
        print("Current balance: " + balance);
    }

    // Subtract points from the point balance.
    public void SubtractFromBalance(int subtrahend)
    {
        balance -= subtrahend;
        scoreDisplay.text = "Points: " + balance.ToString();
    }
}
