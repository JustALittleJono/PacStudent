using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBorderMovement : MonoBehaviour
{
    public Transform ghost1, ghost2, ghost3, ghost4, pacStudent; // Assign ghosts in the inspector
    public float speed = 5f; // Speed in units per second
    private Vector3[] points;
    
    void Start()
    {
        points = new Vector3[]
        {
            Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.5f, 10)), // Left Edge
            Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, 10)), // Top-left corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, 10)), // Top-right corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.1f, 10)), // Bottom-right corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 10))  // Bottom-left corner
        };

        // Start coroutines for each ghost without delay
        StartCoroutine(MoveGhost(ghost1));
        StartCoroutine(MoveGhost(ghost2));
        StartCoroutine(MoveGhost(ghost3));
        StartCoroutine(MoveGhost(ghost4));
        StartCoroutine(MoveGhost(pacStudent));
    }

    IEnumerator MoveGhost(Transform ghost)
    {
        int currentPoint = 0;

        while (true)
        {
            // Move to the current corner
            yield return StartCoroutine(MoveToPoint(ghost, points[currentPoint]));

            // Update to the next corner (clockwise)
            currentPoint = (currentPoint + 1) % 5;
        }
    }

    IEnumerator MoveToPoint(Transform ghost, Vector3 target)
    {
        // Move until the target is reached or exceeded
        while (true)
        {
            // Calculate direction to target
            Vector3 direction = (target - ghost.position).normalized;

            // Calculate the movement step based on speed
            Vector3 moveStep = direction * speed * Time.deltaTime;

            // Check if we would reach or pass the target with the current step
            if (Vector3.Distance(ghost.position, target) <= moveStep.magnitude)
            {
                ghost.position = target; // Snap to target
                yield break; // Exit the coroutine once the target is reached
            }

            // Apply movement step
            ghost.position += moveStep;

            yield return null; // Wait until the next frame
        }
    }
}
