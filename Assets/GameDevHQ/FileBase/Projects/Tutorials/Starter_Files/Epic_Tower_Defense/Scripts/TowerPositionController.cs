using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class TowerPositionController : MonoBehaviour
    {

        private bool _isAvailable = true;
        [SerializeField]
        private GameObject _particle;
        [SerializeField]
        private GameObject _currentTower;
        [SerializeField]
        private string _currentTowerName;
        [SerializeField]
        private bool _isSpotSelected = false;
        [SerializeField]
        private bool _isUpgraded = false;
        [SerializeField]
        private int _towerCostRefund;

        public static event Action onBuyingTower;
        public static Func<string> onSelectingTower;

        private void OnEnable()
        {
            _particle.SetActive(false);
            TowerManager.onPlacingTowers += TowerManager_onPlacingTowers;
            TowerManager.onPlacingTowersFinished += TurnOffParticles;

            UIManager.OnDismantlingTower += DismantleCurrentTower;
            TowerManager.onGetActiveSpot += GetSpot;




        }

        private void TowerManager_onPlacingTowers()
        {
            if (_isAvailable == true)
            {
                _particle.SetActive(true);
            }

        }

        private void OnMouseEnter()
        {
            //_isSpotSelected = false;
            if (_isAvailable == true)
            {
                //Snap to position
                //Tuen radius green
                TowerManager.Instance.SnapToPosition(transform.position);
            }
        }

        private void OnMouseDown()
        {

            if (_isAvailable == true && TowerManager.Instance.CanPlaceTower() == true)
            {
                //try to place tower
                if (onBuyingTower != null)
                {
                    onBuyingTower();
                }

                _currentTower = TowerManager.Instance.PlaceTower(transform.position);
                _isAvailable = false;
                _particle.SetActive(false);

            }

            else
            {
                if (_isAvailable == false)
                {
                    _isSpotSelected = true;
                    GameManager.OnSellingTower += GetTowerValueInWarFunds;
                    TowerManager.OnUpgradingGatlingGun += GetCurrentTower;
                    TowerManager.OnUpgradingMissile += GetCurrentTower;
                    TowerManager.OnGettingTowerPosition += GetCurrentTowerPos;
                    //this is called when try to upgrade
                    //display upgrade UI
                    _currentTowerName = _currentTower.name;
                    //AttackRadius.ReturnTowerType += GetTowerType;

                    if (_currentTowerName == "Gatling_Gun(Clone)")
                    {
                        _towerCostRefund = 150;
                        UIManager.Instance.ActivateUpgradeGatlingGun();

                    }
                    if (_currentTowerName == "Missile_Launcher_Turret(Clone)")
                    {
                        _towerCostRefund = 400;
                        UIManager.Instance.ActivateUpgradeMissile();

                    }
                    if (_isUpgraded)
                    {

                    }
                }
            }
        }

        public void Upgraded(TowerPositionController spot, GameObject tower)
        {
            if (spot == this)
            {
                _currentTower = tower;
            }
        }

        private void OnMouseExit()
        {
            //Unsnap tower postion
            //turn radius red
            TowerManager.Instance.UnSnapPosition();
        }

        public void DismantleCurrentTower()
        {
            if (_isSpotSelected == true)
            {
                Destroy(_currentTower);
            }
        }

        public bool RestoreAvailableSpot()
        {
            return _isAvailable = true;
        }

        void TurnOffParticles()
        {
            _particle.SetActive(false);
        }

        public int GetTowerValueInWarFunds()
        {
            if (_isSpotSelected == true)
            {
                RestoreAvailableSpot();
                return _towerCostRefund;
            }
            else
            {
                return 0;
            }

        }

        public string GetCurrentTower()
        {
            if (_isSpotSelected == true)
            {
                return _currentTowerName;
            }
            else
            {
                Debug.Log("GetCurrentTower is not returning anything");
                return null;
            }
        }

        public Vector3 GetCurrentTowerPos()
        {
            if (_isSpotSelected == true)
            {
                _isUpgraded = true;
                return gameObject.transform.position;
            }
            else
            {
                Debug.Log("GetCurrentTowerPos is not returning anything");
                return Vector3.zero;
            }
        }

        public TowerPositionController GetSpot()
        {
            if (_isSpotSelected == true)
            {
                return this;
            }
            return null;
        }

        public string GetTowerType()
        {
            return _currentTowerName;
        }

        private void OnDisable()
        {
            TowerManager.onPlacingTowers -= TowerManager_onPlacingTowers;
            TowerManager.onPlacingTowersFinished -= TurnOffParticles;
        }



    }
}

