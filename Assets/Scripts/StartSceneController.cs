using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Scene1"); // Change name to your actual scene name
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("InnovationScene"); // Change name to your actual scene name
    }
}