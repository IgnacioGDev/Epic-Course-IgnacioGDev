using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

        [SerializeField]
        private int _towerSelected = 0;
        [SerializeField]
        private string _buttonName;

        public static event Action onGatlingClick;

        private void OnEnable()
        {
            TowerManager.onCancelTowers += CancelTowers;
        }

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

        public void SelectTower(Button btn)
        {
            _buttonName = btn.name;

            if (btn.name == "Gatling")
            {
                _towerSelected = 1;
            }
            if (btn.name == "Missile")
            {
                _towerSelected = 2;
            }            
        }

        public void CancelTowers()
        {
            _towerSelected = 0;
        }

        public int GetItemPicked()
        {
            return _towerSelected;
        }
    }
}

