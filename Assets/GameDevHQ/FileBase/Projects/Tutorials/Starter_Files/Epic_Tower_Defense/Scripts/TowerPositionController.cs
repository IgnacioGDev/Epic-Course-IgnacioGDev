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

        public static event Action onBuyingTower;
        public static Func<string> onSelectingTower;

        private void OnEnable()
        {
            _particle.SetActive(false);
            TowerManager.onPlacingTowers += TowerManager_onPlacingTowers;
            TowerManager.onPlacingTowersFinished += TurnOffParticles;
            UIManager.OnTowerSelected += GetTowerName;
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
            if (_isAvailable == true )
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

                TowerManager.Instance.PlaceTower(transform.position);
                _isAvailable = false;
                _particle.SetActive(false);
            }

            if (onSelectingTower != null)
            {

                Debug.Log(onSelectingTower());
            }

        }

        private void OnMouseExit()
        {
            //Unsnap tower postion
            //turn radius red
            TowerManager.Instance.UnSnapPosition();
        }

        void TurnOffParticles()
        {
            _particle.SetActive(false);
        }

        private void OnDisable()
        {
            TowerManager.onPlacingTowers -= TowerManager_onPlacingTowers;
            TowerManager.onPlacingTowersFinished -= TurnOffParticles;
        }

        public string GetTowerName()
        {
            return onSelectingTower();
        }

    }
}

