using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Level");
    }
}
