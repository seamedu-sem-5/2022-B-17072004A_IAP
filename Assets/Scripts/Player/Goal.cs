using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Public variable to determine which player the goal belongs to
    public bool isPlayer1Goal;

    // Called when an object enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the goal has the "Ball" tag
        if (collision.gameObject.tag == "Ball")
        {
            // If it's not Player 1's goal, Player 1 has scored
            if (!isPlayer1Goal)
            {
                // Calls the Player1Scored method from the GameManager
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
            }
            else
            {
                // If it is Player 1's goal, then Player 2 has scored
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
            }
        }
    }
}
