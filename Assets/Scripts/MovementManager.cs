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
        // Initialize tweener and rotation
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
        HandleInput(); // keyboard inputs to change direction

        // Move the item based on the current direction
        movement = direction * moveSpeed * Time.deltaTime;
        item.transform.position += movement;
    } 

     //Handle keyboard input for changing direction
     void HandleInput()
     {
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             direction = Vector3.left;
             itemRotation.rotateLeft();
         }
         else if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             direction = Vector3.right;
             itemRotation.rotateRight();
         }
         else if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             direction = Vector3.up;
             itemRotation.rotateUp();
         }
         else if (Input.GetKeyDown(KeyCode.DownArrow))
         {
             direction = Vector3.down;
             itemRotation.rotateDown();
         }
    }
}