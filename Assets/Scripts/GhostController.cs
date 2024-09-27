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
        //left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetInteger("Facing", 0);
            animator2.SetInteger("Facing", 0);
        }
        // Down
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("Facing", 1); 
            animator2.SetInteger("Facing", 1);
        }
        // Right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("Facing", 2); 
            animator2.SetInteger("Facing", 2);
        }
        // Up
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetInteger("Facing", 3); 
            animator2.SetInteger("Facing", 3);
        }
    }
}
