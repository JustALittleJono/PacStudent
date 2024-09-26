using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private List<GameObject> itemList = new List<GameObject>();
    private int i;
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
            Vector3 movement = Vector3.zero;

            switch (i)
            {
                case 1:
                    movement = new Vector3(-12.5f, 13f, 0.0f); // Move left
                    break;
                case 2:
                {
                    movement = new Vector3(-7.5f, 13f, 0.0f); // Move right
                    break;
                }
                case 3:
                {
                    movement = new Vector3(-7.5f, 9f, 0.0f); // Move backward
                    break;
                }
                case 4:
                {
                    movement = new Vector3(-12.5f, 9f, 0.0f); // Move forward
                    i = 0;
                    break;
                }
            } i++;

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

    void moveRight()
    {
        movement = new Vector3(-12.5f, 13f, 0.0f);
    }
}
