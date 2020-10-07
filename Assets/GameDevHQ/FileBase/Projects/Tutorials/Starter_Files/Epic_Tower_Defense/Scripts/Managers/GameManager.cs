using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        //Singleton atribute
        private static GameManager _instance;

        //Singleton property
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GAME MANAGER CANNOT BE EMPTY!!");
                }
                return _instance;
            }
        }

        [SerializeField]
        private int countdown = 10;
        [SerializeField]
        private float lives = 100f;
        [SerializeField]
        private Transform _endZoneTrigger;
        [SerializeField]
        private Text _inGameFunds;
        private int _warFunds;

        public static event Func<int> OnSellingTower;
        public static event Func<int> OnAddLootedFunds;

        //Singleton instantiation
        private void Awake()
        {
            _instance = this;

        }

        private void Start()
        {
            StartCoroutine(CountdownToExtinction());

            _warFunds = 900;
        }

        private void OnEnable()
        {
            TowerPositionController.onBuyingTower += ChargeWarFunds;
        }

        private void Update()
        {
            _inGameFunds.text = _warFunds.ToString();
            Debug.Log("ADD WAR FRUNDS: " + _warFunds);
            NormalizeNegativeFunds();

            if (Input.GetKeyDown(KeyCode.T))
            {
                PauseGame();
            }
        }

        private void NormalizeNegativeFunds()
        {
            if (_warFunds < 0)
            {
                _warFunds = 0;
            }
        }

        public Transform GetEndZone()
        {
            return _endZoneTrigger;
        }

        public int GetWarFunds()
        {
            return Mathf.RoundToInt(_warFunds);
        }

        public void ChargeWarFunds()
        {
            if (_warFunds > 0)
            {
                _warFunds -= 250;
            }
            else
            {
                Debug.Log("NOT ENOUGH WAR FUNDS");
            }

        }

        //Method being used in UI in "selling tower" button
        public void AddWarFunds()
        {
            if (OnSellingTower != null)
            {
                _warFunds += OnSellingTower();

            }
        }

        //Method to add funds to warFunds when an enemy is killed
        //public int AddLootedFunds()
        //{
        //    if (OnAddLootedFunds != null)
        //    {   
        //        return _warFunds += OnAddLootedFunds();             
        //    }
        //    else
        //    return _warFunds;
        //}

        public void AddLootedFunds(int loot)
        {
            _warFunds += loot;
        }

        public void LifeIndicator()
        {
            lives = lives - 5;
        }

        public float GetAmountOfLives()
        {
            return lives;
        }

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
        }

        public void AccelerateGameSpeed()
        {
            Time.timeScale = 2.0f;
        }

        IEnumerator CountdownToExtinction()
        {
            while (countdown > 0)
            {
                Debug.Log(countdown);
                yield return new WaitForSeconds(1f);
                countdown--;
            }
            
        }

        private void OnDisable()
        {
            TowerPositionController.onBuyingTower -= ChargeWarFunds;
        }




    }

}
