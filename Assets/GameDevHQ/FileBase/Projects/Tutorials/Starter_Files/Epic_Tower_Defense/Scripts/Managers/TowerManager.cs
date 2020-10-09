using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif



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
                    //Debug.LogError("TowerPlacementController is NULL");
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
        [SerializeField]
        private GameObject _gatlingGunUpgrade;
        [SerializeField]
        private GameObject _missileUpgrade;
        [SerializeField]
        private GameObject _upgradedTower;

        [SerializeField]
        private RaycastHit _checkCollider;


        private int _activeDecoyIndex;
        private bool _canPlaceTower = false;
        private bool _isDecoyActive = false;
        public static event Action onPlacingTowers;
        public static event Action onPlacingTowersFinished;
        public static event Action onCancelTowers;
        public static event Func<string> OnUpgradingGatlingGun;
        public static event Func<string> OnUpgradingMissile;
        public static event Func<Vector3> OnGettingTowerPosition;
        public static event Action<TowerPositionController, GameObject> onUpgradeComplete;
        public static event Func<TowerPositionController> onGetActiveSpot;


        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {

        }


        // Update is called once per frame
        void Update()
        {
            //InstantiateTowersDeprecated();
            InstantiateTowers(UIManager.Instance.GetItemPicked());

        }

        private void InstantiateTowersDeprecated()
        {
            if (GameManager.Instance.GetWarFunds() >= 250)
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
            else
            {
                _decoyTowers[0].SetActive(false);
                _decoyTowers[1].SetActive(false);
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

        public GameObject PlaceTower(Vector3 spotPos)
        {
            return Instantiate(_towerPrefabs[_activeDecoyIndex], spotPos, Quaternion.identity);
        }

        public bool CanPlaceTower()
        {
            _isDecoyActive = _decoyTowers[_activeDecoyIndex].activeSelf;
            return _isDecoyActive;
        }

        private void InstantiateTowers(int towerID)
        {
            if (GameManager.Instance.GetWarFunds() >= 250)
            {

                if (towerID == 1) //Gatling gun 
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
                if (towerID == 2) //Missile launcher
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
                    if (onCancelTowers != null)
                    {
                        onCancelTowers();
                    }
                }


                if (_canPlaceTower == false)
                {
                    TowerUnderMouseMovement();
                }
            }
            else
            {
                _decoyTowers[0].SetActive(false);
                _decoyTowers[1].SetActive(false);
            }
        }

        public void InstantiateUpgrades()
        {
            if (OnUpgradingGatlingGun != null)
            {
                if (OnUpgradingGatlingGun() == "Gatling_Gun(Clone)")
                {
                    TowerPositionController spot = onGetActiveSpot();
                    _upgradedTower = Instantiate(_gatlingGunUpgrade, OnGettingTowerPosition(), Quaternion.identity);
                    //Debug.Log("UPDATING THE GATLING GUN");
                    if (onUpgradeComplete != null)
                    {
                        onUpgradeComplete(spot, _upgradedTower);
                    }

                }
            }
            if (OnUpgradingMissile != null)
            {
                if (OnUpgradingMissile() == "Missile_Launcher_Turret(Clone)")
                {
                    _upgradedTower = Instantiate(_missileUpgrade, OnGettingTowerPosition(), Quaternion.identity);
                    //Debug.Log("UPDATING THE MISSILE LAUNCHER");
                }
            }
        }

        public GameObject GetUpgradedTower()
        {
            return _upgradedTower;
        }

    }

}
