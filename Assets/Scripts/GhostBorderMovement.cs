using System.Collections;
using UnityEngine;

public class GhostBorderMovement : MonoBehaviour
{
    public Transform ghost1, ghost2, ghost3, ghost4; // Assign ghosts in the inspector
    public Transform pacStudent; // Assign PacStudent in the inspector
    public float speed = 5f; // Speed in units per second
    private Vector3[] corners; // Corners for clockwise movement

    void Start()
    {
        // Define corner positions based on updated screen boundaries
        corners = new Vector3[]
        {
            Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.5f, 10)), // Bottom-left corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.95f, 10)), // Top-left corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 10)), // Top-right corner
            Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.5f, 10)), // Bottom-right corner
        };

        // Start coroutines for each ghost without delay
        StartCoroutine(MoveGhost(ghost1));
        StartCoroutine(MoveGhost(ghost2));
        StartCoroutine(MoveGhost(ghost3));
        StartCoroutine(MoveGhost(ghost4));

        // Start coroutine for PacStudent without delay
        StartCoroutine(MovePacStudent(pacStudent));
    }

    IEnumerator MoveGhost(Transform ghost)
    {
        // Get the Animator components for both the body and eyes of the ghost
        Animator bodyAnimator = ghost.GetComponentInChildren<Animator>();
        Animator eyesAnimator = ghost.Find("Eyes").GetComponent<Animator>();

        if (bodyAnimator == null || eyesAnimator == null)
        {
            Debug.LogError("Animator component missing in Body or Eyes for ghost: " + ghost.name);
            yield break; // Exit coroutine if Animators are missing
        }
        
        int currentCorner = 0;
        while (true)
        {
            // Move to the current corner
            yield return StartCoroutine(MoveToPoint(ghost, corners[currentCorner], bodyAnimator, eyesAnimator));

            // Update to the next corner (clockwise)
            currentCorner = (currentCorner + 1) % 4;
        }
    }

    IEnumerator MovePacStudent(Transform pacStudent)
    {
        Animator pacStudentAnimator = pacStudent.GetComponent<Animator>();

        if (pacStudentAnimator == null)
        {
            Debug.LogError("Animator component missing for PacStudent: " + pacStudent.name);
            yield break; // Exit coroutine if Animator is missing
        }
        // After reaching the left edge, start moving clockwise
        int currentCorner = 0;

        while (true)
        {
            // Move to the current corner
            yield return StartCoroutine(MoveToPoint(pacStudent, corners[currentCorner], pacStudentAnimator));

            // Update to the next corner (clockwise)
            currentCorner = (currentCorner + 1) % 4;
        }
    }

    IEnumerator MoveToPoint(Transform character, Vector3 target, Animator animator)
    {
        Vector3 direction = (target - character.position).normalized; // Get the direction to move in

        // Update the facing direction parameter before moving
        UpdateFacingParameter(direction, animator);

        while (character.position != target)
        {
            character.position = Vector3.MoveTowards(character.position, target, speed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }
    }

    IEnumerator MoveToPoint(Transform ghost, Vector3 target, Animator bodyAnimator, Animator eyesAnimator)
    {
        Vector3 direction = (target - ghost.position).normalized; // Get the direction to move in

        // Update the facing direction parameter before moving
        UpdateFacingParameter(direction, bodyAnimator, eyesAnimator);

        while (ghost.position != target)
        {
            ghost.position = Vector3.MoveTowards(ghost.position, target, speed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }
    }

    void UpdateFacingParameter(Vector3 direction, Animator bodyAnimator, Animator eyesAnimator)
    {
        int facing = 0;

        if (direction.x < 0) // Moving left
        {
            facing = 0;
        }
        else if (direction.y < 0) // Moving down
        {
            facing = 1;
        }
        else if (direction.x > 0) // Moving right
        {
            facing = 2;
        }
        else if (direction.y > 0) // Moving up
        {
            facing = 3;
        }

        // Update the Facing parameter in both animators
        bodyAnimator.SetInteger("Facing", facing);
        eyesAnimator.SetInteger("Facing", facing);
    }

    void UpdateFacingParameter(Vector3 direction, Animator animator)
    {
        int facing = 0;

        if (direction.x < 0) // Moving left
        {
            facing = 0;
        }
        else if (direction.y < 0) // Moving down
        {
            facing = 1;
        }
        else if (direction.x > 0) // Moving right
        {
            facing = 2;
        }
        else if (direction.y > 0) // Moving up
        {
            facing = 3;
        }

        // Update the Facing parameter in the animator
        animator.SetInteger("Facing", facing);
    }
}
