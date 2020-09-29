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
        private GameObject _upgradeSprite;

        public static event Func<string> OnTowerSelected;

        //public static event Action onGatlingClick;

        private void OnEnable()
        {
            TowerManager.onCancelTowers += CancelTowers;
        }

        private void Awake()
        {
            _instance = this;
        }

        public void SelectTower(Button btn)
        {
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

        public void ActivateUpgrade()
        {
            _upgradeSprite.SetActive(true);

        }
    }
}

