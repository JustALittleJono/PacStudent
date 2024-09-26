using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private List<GameObject> itemList = new List<GameObject>();

    private Tweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        
        tweener = GetComponent<Tweener>();
        if (item is not null)
        {
            itemList.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject newItem = Instantiate(item, new Vector3(0, 0.5f, 0), Quaternion.identity);
                itemList.Add(newItem);
            }

            Vector3 movement = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.A))
            {
                movement = new Vector3(-2.0f, 0.5f, 0.0f); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                movement = new Vector3(2.0f, 0.5f, 0.0f); // Move right
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                movement = new Vector3(0.0f, 0.5f, -2.0f); // Move backward
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                movement = new Vector3(0.0f, 0.5f, 2.0f); // Move forward
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
}
