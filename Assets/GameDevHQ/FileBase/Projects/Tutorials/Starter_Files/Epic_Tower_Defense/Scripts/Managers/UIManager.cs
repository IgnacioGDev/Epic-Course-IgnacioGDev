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
        private Sprite[] _uiSprites;
        [SerializeField]
        private Image _playBackSprite;
        [SerializeField]
        private Image _armorySprite;
        [SerializeField]
        private Image _warfundsSprite;
        [SerializeField]
        private Image _livesHubSprite;
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
        [SerializeField]
        private Text _livesText;

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

        private void Update()
        {
            UpdateLifeUI();
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

        private void UpdateLifeUI()
        {
            _livesText.text = GameManager.Instance.GetAmountOfLives().ToString();

            if (GameManager.Instance.GetAmountOfLives() < 85)
            {
                _playBackSprite.GetComponent<Image>().sprite = _uiSprites[4];
                _armorySprite.GetComponent<Image>().sprite = _uiSprites[0];
                _warfundsSprite.GetComponent<Image>().sprite = _uiSprites[8];
                _livesHubSprite.GetComponent<Image>().sprite = _uiSprites[2];
            }
            if (GameManager.Instance.GetAmountOfLives() < 70)
            {
                _playBackSprite.GetComponent<Image>().sprite = _uiSprites[5];
                _armorySprite.GetComponent<Image>().sprite = _uiSprites[1];
                _warfundsSprite.GetComponent<Image>().sprite = _uiSprites[9];
                _livesHubSprite.GetComponent<Image>().sprite = _uiSprites[3];
            }
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

