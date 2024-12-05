using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets; // List of targets
    public Vector3 offset; // Offset from the center of targets
    private Vector3 velocity = Vector3.zero; // For SmoothDamp
    public float damping = 0.3f; // Smooth time

    private void Update()
    {
        if (targets == null)
        {
            targets = new List<Transform>();
        }

        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
       
            foreach (GameObject go in g)
            {
                Transform t = go.transform;

                if(!targets.Contains(t))
                {
                    targets.Add(t);
                }
            }
    }

    private void FixedUpdate()
    {
        if (targets.Count == 0) return; // If there are no targets, do nothing

        // Calculate the average position of all targets
        Vector3 centerPoint = Vector3.zero;
        foreach (Transform target in targets)
        {
            centerPoint += target.position;
        }
        centerPoint /= targets.Count; // Average position of targets

        // Calculate the desired position with the offset
        Vector3 targetPos = centerPoint + offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, damping);

        // Camera Adjustment based on the distance between two targets
        if(targets.Count > 1)
        {
            float distance = Vector3.Distance(targets[0].position, targets[1].position);
            Camera.main.orthographicSize = distance;
            if(Camera.main.orthographicSize >= 10f)
            {
                Camera.main.orthographicSize = 10f;
            }
        }
    }
}
