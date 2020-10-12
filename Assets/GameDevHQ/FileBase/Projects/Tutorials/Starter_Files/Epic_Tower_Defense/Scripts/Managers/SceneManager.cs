using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scripts.Managers
{
    public class SceneManager : MonoBehaviour
    {
        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("Scene manager can't be empty!!!");
                    return null;
                }
                else
                {
                    return _instance;
                }
            }
        }

        private void Awake()
        {
            _instance = this;
        }


        public void RestartScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Level");
        }

    }
}

