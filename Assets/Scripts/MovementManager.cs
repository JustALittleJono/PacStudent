using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private GameObject item; // Object to move
    private List<GameObject> itemList = new List<GameObject>();
    private Tweener tweener;
    private Vector3 movement;
    private Rotation itemRotation;

    public float moveSpeed = 4f; // Speed for movement
    private Vector3 direction; // Current movement direction (left, right, up, down)

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();

        if (item != null)
        {
            itemList.Add(item);
            itemRotation = item.GetComponent<Rotation>();
        }

        direction = Vector3.left; // Initial movement direction (left)
        itemRotation.rotateLeft(); // Start facing left
    }

    // Update is called once per frame
    void Update()
    {
        // HandleInput(); // keyboard inputs to change direction

        // Move the item based on the current direction
        movement = direction * moveSpeed * Time.deltaTime;
        item.transform.position += movement;

        // Here you can add a condition to check when to change direction, e.g., reaching a boundary or point
        CheckBoundaryOrCondition();
    }

    // Handle keyboard input for changing direction
    // void HandleInput()
    // {
    //     if (Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         direction = Vector3.left;
    //         itemRotation.rotateLeft();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         direction = Vector3.right;
    //         itemRotation.rotateRight();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         direction = Vector3.up;
    //         itemRotation.rotateUp();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         direction = Vector3.down;
    //         itemRotation.rotateDown();
    //     }
    // }

    // Check for conditions like hitting boundaries, walls, or other triggers to change direction
    void CheckBoundaryOrCondition()
    {
        // Example: If the object hits a boundary, change direction
        if (item.transform.position is { x: <= -12.5f, y: >= 9f })
        {
            direction = Vector3.down; // Change direction to down
            itemRotation.rotateDown();
        }
        else if (item.transform.position is { x: >= -7.5f, y: <= 13f })
        {
            direction = Vector3.up; // Change direction to up
            itemRotation.rotateUp();
        }
        else if (item.transform.position.y >= 13f)
        {
            direction = Vector3.left; // Change direction to left
            itemRotation.rotateLeft();
        }
        else if (item.transform.position is { y: <= 9f, x: <= -7.5f })
        {
            direction = Vector3.right; // Change direction to right
            itemRotation.rotateRight();
        }
    }
}