using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    //[SerializeField]private GameObject pacMonster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
    }

    void detectInput()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.identity;
            if (transform.localScale.x < 0)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,90);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.identity;
            if (transform.localScale.x > 0)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,-90);
        }
    }
}
