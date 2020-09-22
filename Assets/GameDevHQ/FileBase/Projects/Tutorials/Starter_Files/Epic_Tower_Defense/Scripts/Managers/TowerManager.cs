using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Scripts.Managers
{
    //Only update decoys
    public class TowerManager : MonoBehaviour
    {
        private static TowerManager _instance;
        public static TowerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("TowerPlacementController is NULL");
                }
                return _instance;
            }
        }

        [SerializeField]
        private GameObject[] _decoyTowers; // 0 = gatling - 1 = missile
        [SerializeField]
        private GameObject[] _towerPrefabs;
        [SerializeField]
        private MeshRenderer[] _towerRadius;


        private int _activeDecoyIndex;
        private bool _canPlaceTower = false;
        private bool _isDecoyActive = false;
        public static event Action onPlacingTowers;
        public static event Action onPlacingTowersFinished;


        private void Awake()
        {
            _instance = this;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //Gatling gun 
            {
                //Enable decoy gatling gun
                _decoyTowers[0].SetActive(true);
                _decoyTowers[1].SetActive(false);
                _activeDecoyIndex = 0;

                if (onPlacingTowers != null)
                {
                    onPlacingTowers();
                }                   

            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //Missile launcher
            {
                //Enable decoy missile launcher
                _decoyTowers[0].SetActive(false);
                _decoyTowers[1].SetActive(true);
                _activeDecoyIndex = 1;

                if (onPlacingTowers != null)
                    onPlacingTowers();

            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                _decoyTowers[_activeDecoyIndex].SetActive(false);
                if (onPlacingTowersFinished != null)
                {
                    onPlacingTowersFinished();
                }
            }


            if (_canPlaceTower == false)
            { 
                TowerUnderMouseMovement();
            }
   
        }

        private void TowerUnderMouseMovement()
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                _decoyTowers[_activeDecoyIndex].transform.position = hitInfo.point;

            }
        }

        public void SnapToPosition(Vector3 spotPos)
        {        
            _decoyTowers[_activeDecoyIndex].transform.position = spotPos;
            _canPlaceTower = true;
            _towerRadius[_activeDecoyIndex].material.color = Color.green;
        }

        public void UnSnapPosition()
        {
            _canPlaceTower = false;
            _towerRadius[_activeDecoyIndex].material.color = Color.red;
        }

        public void PlaceTower(Vector3 spotPos)
        {
            Instantiate(_towerPrefabs[_activeDecoyIndex], spotPos, Quaternion.identity);
        }

        public bool CanPlaceTower()
        {
            _isDecoyActive = _decoyTowers[_activeDecoyIndex].activeSelf;
            return _isDecoyActive;
        }

    }

}
