using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadandWriteSystem : MonoBehaviour
{
    // References to the UI Input Fields for player names
    public TMP_InputField player1InputField; // Input field for Player 1's name
    public TMP_InputField player2InputField; // Input field for Player 2's name

    // References to the UI elements displaying the scores
    public GameObject player1Score; // UI element displaying Player 1's score
    public GameObject player2Score; // UI element displaying Player 2's score

    // Reference to the GameManager to get and set game-related data
    public GameManager gameManager;

    // Method to save player data to a JSON file
    public void SaveToJson()
    {
        // Create a new instance of PlayerData to store the data
        PlayerData data = new PlayerData();

        // Parse and assign Player 1's score from the UI to PlayerData
        data.player1Score = int.Parse(player1Score.GetComponent<TextMeshProUGUI>().text);

        // Parse and assign Player 2's score from the UI to PlayerData
        data.player2Score = int.Parse(player2Score.GetComponent<TextMeshProUGUI>().text);

        // Assign player names from the input fields to PlayerData
        data.player1Name = player1InputField.text;
        data.player2Name = player2InputField.text;

        // Update GameManager's player names with the text from input fields
        gameManager.player1Name.text = player1InputField.text;
        gameManager.player2Name.text = player2InputField.text;

        // Convert the PlayerData object to a JSON string with formatting for readability
        string json = JsonUtility.ToJson(data, true);

        // Save the JSON string to a file in the specified directory
        File.WriteAllText(Application.dataPath + "/JsonFile/PlayerDataFile.json", json);
    }

    // Method to load player data from the JSON file
    public void LoadFromJson()
    {
        // Read the JSON string from the file
        string json = File.ReadAllText(Application.dataPath + "/JsonFile/PlayerDataFile.json");

        // Convert the JSON string back into a PlayerData object
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        // Update the score UI elements with the values from the loaded data
        player1Score.GetComponent<TextMeshProUGUI>().text = data.player1Score.ToString();
        player2Score.GetComponent<TextMeshProUGUI>().text = data.player2Score.ToString();

        // Update the input fields with the player names from the loaded data
        player1InputField.text = data.player1Name;
        player2InputField.text = data.player2Name;

        // Update the GameManager's player score data
        gameManager.player1Score = data.player1Score;
        gameManager.player2Score = data.player2Score;
    }
}
