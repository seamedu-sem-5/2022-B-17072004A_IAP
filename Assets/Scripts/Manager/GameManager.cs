using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball; // Reference to the ball object

    [Header("Player1")]
    public GameObject player1Paddle; // Reference to Player 1's paddle
    public GameObject player1Goal; // Reference to Player 1's goal
    public TMP_Text player1Name; // Reference to Player 1's name (for UI)

    [Header("Player2")]
    public GameObject player2Paddle; // Reference to Player 2's paddle
    public GameObject player2Goal; // Reference to Player 2's goal
    public TMP_Text player2Name; // Reference to Player 2's name (for UI)

    [Header("ScoreUI")]
    public GameObject player1ScoreTxt; // UI Text element to display Player 1's score
    public GameObject player2ScoreTxt; // UI Text element to display Player 2's score
    public GameObject canvas1; // The UI canvas that shows during the game
    public GameObject gameField; // The main game field that contains paddles and ball

    [Header("Leaderboard")]
    public GameObject leaderboardPanel; // UI panel that shows when the game ends (leaderboard)

    public int player1Score; // Player 1's score
    public int player2Score; // Player 2's score

    Vector2 startPosition; // The starting position for the ball

    private void Start()
    {
        startPosition = ball.transform.position; // Store the initial position of the ball
        gameField.SetActive(false); // Hide the game field initially
        canvas1.SetActive(true); // Show the UI canvas initially
        leaderboardPanel.SetActive(false); // Hide the leaderboard initially
    }

    private void Update()
    {
        // If either player reaches 5 points, end the game and show the leaderboard
        if (player1Score >= 5 || player2Score >= 5)
        {
            gameField.SetActive(false); // Hide the game field
            canvas1.SetActive(false); // Hide the game UI
            leaderboardPanel.SetActive(true); // Show the leaderboard
        }
    }

    // Method called when Player 1 scores a point
    public void Player1Scored()
    {
        player1Score++; // Increase Player 1's score
        player1ScoreTxt.GetComponent<TextMeshProUGUI>().text = player1Score.ToString(); // Update the score UI
        ResetPosition(); // Reset the ball position after scoring
    }

    // Method called when Player 2 scores a point
    public void Player2Scored()
    {
        player2Score++; // Increase Player 2's score
        player2ScoreTxt.GetComponent<TextMeshProUGUI>().text = player2Score.ToString(); // Update the score UI
        ResetPosition(); // Reset the ball position after scoring
    }

    // Method to reset the ball's position to the starting position and relaunch it
    public void ResetPosition()
    {
        ball.transform.position = startPosition; // Set the ball's position back to the starting point
        ball.GetComponent<Ball>().Launch(); // Launch the ball again
    }

    // Method to hide the UI canvas and show the game field again
    public void EscapeUI()
    {
        canvas1.SetActive(false); // Hide the UI canvas
        gameField.SetActive(true); // Show the game field
    }

    // Method to restart the game by loading the first scene
    public void Restart()
    {
        SceneManager.LoadScene(0); // Reload the initial scene (reset the game)
    }
}
