using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private Animator animator2;

    void Start()
    {
    }

    void Update()
    {
        HandleMovement();
    }
    
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetInteger("Facing", 0); // Left
            animator2.SetInteger("Facing", 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("Facing", 1); // Down
            animator2.SetInteger("Facing", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("Facing", 2); // Right
            animator2.SetInteger("Facing", 2);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetInteger("Facing", 3); // Up
            animator2.SetInteger("Facing", 3);
        }
    }

    void facing()
    {
        
    }
}
