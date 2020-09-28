using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIManager can't be NULL");
                }
                return _instance;
            }
        }


        public static event Action onGatlingClick;

        private void Awake()
        {
            _instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (onGatlingClick != null)
            //{
            //    onGatlingClick();
            //}
        }
    }
}

