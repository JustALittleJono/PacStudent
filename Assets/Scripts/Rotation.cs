using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void rotateLeft()
    {
        //reset rotation
        transform.rotation = Quaternion.identity;
        if (transform.localScale.x < 0)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, 
                             transform.localScale.y, 
                             transform.localScale.z);
        }
    }

    public void rotateDown()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
        if (transform.localScale.x < 0)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, 
                             transform.localScale.y, 
                             transform.localScale.z);
        }
    }

    public void rotateRight()
    {
        transform.rotation = Quaternion.identity;
        if (transform.localScale.x > 0)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, 
                             transform.localScale.y, 
                             transform.localScale.z);
        }
    }

    public void rotateUp()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        if (transform.localScale.x < 0)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, 
                             transform.localScale.y,
                             transform.localScale.z);
        }
    }
}