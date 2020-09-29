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
        private Image _gatlingImage;
        [SerializeField]
        private Image _missile;

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
            if (OnTowerSelected != null)
            {
                if (OnTowerSelected() != "Gatling_Gun(Clone)")
                {
                    Debug.Log("UI MANAGER // GATLING GUN");

                }
                if (OnTowerSelected() != "Missile_Launcher_Turret(Clone)")
                {
                    Debug.Log("UI MANAGER // MISSILE LAUCHER");
                }
            }
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
    }
}

