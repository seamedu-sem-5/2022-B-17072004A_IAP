using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;    // Duration of the shake
    public float shakeMagnitude = 0.2f;  // Intensity of the shake
    public float dampingSpeed = 1.5f;    // Speed at which the shake dampens

    private float currentShakeTime = 0f; // Tracks remaining shake time
    private float currentShakeMagnitude; // Current intensity of the shake

    private void Update()
    {
        // If shaking, apply the effect
        if (currentShakeTime > 0)
        {
            ApplyShake();
        }
        if(Camera.main.orthographicSize <= 5f)
        {
            Camera.main.orthographicSize = 5f;
        }
    }

    public void StartShake()
    {
        currentShakeTime = shakeDuration; // Reset the shake timer
        currentShakeMagnitude = shakeMagnitude; // Reset the shake magnitude
    }

    private void ApplyShake()
    {
        // Decrease the shake timer
        currentShakeTime -= Time.deltaTime;

        // Generate random offsets
        float xOffset = Random.Range(-1f, 1f) * currentShakeMagnitude;
        float yOffset = Random.Range(-1f, 1f) * currentShakeMagnitude;

        // Apply the random offsets relative to the current camera position
        transform.localPosition += new Vector3(xOffset, yOffset, 0);

        // Gradually dampen the shake magnitude
        currentShakeMagnitude = Mathf.Lerp(currentShakeMagnitude, 0f, Time.deltaTime * dampingSpeed);

        // Stop shaking when time runs out
        if (currentShakeTime <= 0)
        {
            currentShakeTime = 0; // Ensure shake doesn't go negative
        }
    }
}
