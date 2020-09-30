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
        private GameObject _gatlingGunUpgradeSprite;
        [SerializeField]
        private GameObject _missilieUpgradeSprite;
        [SerializeField]
        private GameObject _gatlingGunSpriteButton;
        [SerializeField]
        private GameObject _missileSpriteButton;
        [SerializeField]
        private GameObject _sellSpriteButton;

        public static event Func<string> OnTowerSelected;
        public static event Action OnDismantlingTower;

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

        public void DismantleTower()
        {
            if (OnDismantlingTower != null)
            {
                OnDismantlingTower();
            }           
        }


        public void CancelTowers()
        {
            _towerSelected = 0;
            DefaultArmoryOptions();
        }

        public int GetItemPicked()
        {
            return _towerSelected;
        }

        public void ActivateUpgradeGatlingGun()
        {
            _gatlingGunUpgradeSprite.SetActive(true);
            _missilieUpgradeSprite.SetActive(false);
            _sellSpriteButton.SetActive(true);
            _gatlingGunSpriteButton.SetActive(false);
            _missileSpriteButton.SetActive(false);

        }

        public void ActivateUpgradeMissile()
        {
            _missilieUpgradeSprite.SetActive(true);
            _gatlingGunUpgradeSprite.SetActive(false);
            _sellSpriteButton.SetActive(true);
            _gatlingGunSpriteButton.SetActive(false);
            _missileSpriteButton.SetActive(false);
        }

        public void DefaultArmoryOptions()
        {
            _gatlingGunUpgradeSprite.SetActive(false);
            _missilieUpgradeSprite.SetActive(false);
            _sellSpriteButton.SetActive(false);
            _gatlingGunSpriteButton.SetActive(true);
            _missileSpriteButton.SetActive(true);
        }
    }
}

