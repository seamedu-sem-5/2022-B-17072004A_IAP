using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Public variable to determine if this is Player 1's paddle
    public bool isPlayer1;

    // Reference to the Rigidbody2D component for physics-based movement
    public Rigidbody2D rb;

    // Speed at which the paddle moves
    public float speed;

    // Variable to store the movement input for the paddle
    private float movement;
   
    void Update()
    {
        // Check if this is Player 1's paddle and get input accordingly
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Vertical"); // Vertical input for Player 1
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical2"); // Vertical input for Player 2 (assumed separate input)
        }

        // Set the velocity of the paddle based on the movement input and speed
        rb.velocity = new Vector2(rb.velocity.x, movement * speed); // Only modifying the y-axis velocity
    }
}
