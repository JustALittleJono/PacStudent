using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private List<GameObject> itemList = new List<GameObject>();
    private int i = 1;
    private Tweener tweener;
    private Vector3 movement;
    private Rotation itemRotation;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        if (item is not null)
        {
            itemList.Add(item);
            itemRotation = item.GetComponent<Rotation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item.transform.position == new Vector3(-7.5f, 13f,transform.position.z))
        {
            i = 1;
        }
        else if (item.transform.position == new Vector3(-12.5f, 13f,transform.position.z))
        {
            i = 2;
        }
        else if (item.transform.position == new Vector3(-12.5f, 9.0f, transform.position.z))
        {
            i = 3;
        }
        else if (item.transform.position == new Vector3(-7.5f, 9.0f,transform.position.z))
        {
            i = 4;
        }

        switch (i)
        {
            case 1:
                moveLeft();
                break;
            case 2:
                moveDown();
                break;

            case 3:
                moveRight();
                break;

            case 4:
                moveUp();
                break;
        }

        if (movement != Vector3.zero)
        {
            foreach (GameObject obj in itemList)
            {
                bool tweenAdded = tweener.AddTween(obj.transform, obj.transform.position, movement, 1.5f);
                if (tweenAdded)
                {
                    break;
                }
            }
        }
    }

    void moveLeft()
    {
        movement = new Vector3(item.transform.position.x-1.0f, item.transform.position.y,item.transform.position.z);
        itemRotation.rotateLeft();
    }
    void moveDown()
    {
        movement = new Vector3(item.transform.position.x, item.transform.position.y-1.0f,item.transform.position.z);
        itemRotation.rotateDown();
    }
    void moveRight()
    {
        movement = new Vector3(item.transform.position.x+1.0f, item.transform.position.y,item.transform.position.z);
        itemRotation.rotateRight();
    }
    void moveUp()
    {
        movement = new Vector3(item.transform.position.x, item.transform.position.y-1.0f,item.transform.position.z);
        itemRotation.rotateUp();
    }
    
}
