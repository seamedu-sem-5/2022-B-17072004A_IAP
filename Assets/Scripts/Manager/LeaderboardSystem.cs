using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class LeaderboardSystem : MonoBehaviour
{
    // References to the UI elements that display the scores and names
    public TMP_Text player1Score; // Text for displaying Player 1's score
    public TMP_Text player2Score; // Text for displaying Player 2's score

    public TMP_Text player1Name; // Text for displaying Player 1's name
    public TMP_Text player2Name; // Text for displaying Player 2's name

    public GameManager gameManager; // Reference to the GameManager script to access scores and names

    void Start()
    {
        // Initialize the scores in the leaderboard by getting values from the GameManager
        player1Score.text = gameManager.player1Score.ToString();
        player2Score.text = gameManager.player2Score.ToString();
    }

    void Update()
    {
        // Compare the scores to determine which player won and update the leaderboard display
        if (gameManager.player1Score > gameManager.player2Score)
        {
            // If Player 1 has a higher score, set their name and score in the leaderboard
            player1Name.text = gameManager.player1Name.text;
            player2Name.text = gameManager.player2Name.text;

            // Update the score displays
            player1Score.text = gameManager.player1Score.ToString();
            player2Score.text = gameManager.player2Score.ToString();
        }
        else
        {
            // If Player 2 has a higher score, swap the names and scores on the leaderboard
            player1Name.text = gameManager.player2Name.text;
            player2Name.text = gameManager.player1Name.text;

            // Update the score displays
            player1Score.text = gameManager.player2Score.ToString();
            player2Score.text = gameManager.player1Score.ToString();
        }
    }
}
