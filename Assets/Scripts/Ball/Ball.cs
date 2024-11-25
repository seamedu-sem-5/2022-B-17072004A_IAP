using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Public variables to control ball speed, Rigidbody, and speed increase rate
    public float currentSpeed; // Current speed of the ball
    public float speed; // Initial speed of the ball when launched
    public Rigidbody2D rb; // Reference to the Rigidbody2D component for physics calculations
    public float speedIncreaseRate; // Rate at which the ball's speed increases over time

    void Start()
    {
        Launch(); // Calls the Launch method to start the ball's movement when the game begins
    }

    void Update()
    {
        // Increase the current speed over time based on the speedIncreaseRate and deltaTime
        currentSpeed += speedIncreaseRate * Time.deltaTime;

        // Adjust the velocity of the ball to match the current speed while keeping the same direction
        rb.velocity = rb.velocity.normalized * currentSpeed;
    }

    // Method to launch the ball in a random direction at the initial speed
    public void Launch()
    {
        // Generate random values for x and y to determine the direction of the ball's velocity
        // -1 or 1 for both axes to give a random direction
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        // Set the current speed to the initial speed
        currentSpeed = speed;

        // Set the velocity of the ball in a random direction, using the current speed and direction
        rb.velocity = new Vector2(currentSpeed * x, currentSpeed * y);
    }
}
