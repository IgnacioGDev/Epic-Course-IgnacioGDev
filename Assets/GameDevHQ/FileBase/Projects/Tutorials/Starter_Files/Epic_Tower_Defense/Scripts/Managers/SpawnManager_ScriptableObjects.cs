using Scripts.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Scripts.Managers

{
    public class SpawnManager_ScriptableObjects : MonoBehaviour
    {
        private static SpawnManager_ScriptableObjects _instance;
        public static SpawnManager_ScriptableObjects Instance
         {
            get
            {
                if (_instance == null)
                    Debug.LogError("SpawnManager_scriptableObjects is NULL");

                return _instance;
            }
        }

        [SerializeField]
        private Transform _startingPoint;
        [SerializeField]
        private int _enemyCounter;
        [SerializeField]
        private bool isOnWave;


        private void Awake()
        {
            _instance = this;
            EndZoneTrigger.onEnemyReachedEnd += OnEndZoneReach;
            
        }

        private int _currentWave;

        private void Start()
        {
            StartCoroutine(SpawnSequence());
        }

        IEnumerator SpawnSequence()
        {
            while (true)
            {
                var delay = PoolManager.Instance.ReturnCurrentWaveDelay();
                if (delay == 200)
                {
                    Debug.Log("GAME COMPLETE");
                    //GAME COMPLETE
                    break;
                }
                yield return new WaitForSeconds(delay);
                var enemy = PoolManager.Instance.RequestEnemy();
                //if enemy is null
                if (enemy != null)
                {
                    enemy.SetActive(true);
                }
                else
                {
                    break;
                }
                
            }
        }

        public Vector3 GetStartPos()
        {
            return _startingPoint.transform.position;
        }


        public void OnEndZoneReach()
        {
            //Debug.Log("NUMERO ACTUAL DE ENEMIGOS DE WAVE ES: " + _enemyCounter);
        }

        public Quaternion InitRotation()
        {
            return _startingPoint.transform.rotation;
        }

        public void StartNextWave()
        {
            _enemyCounter = 0;
            StartCoroutine(SpawnSequence());
        }

        public int GetCurrentEnemiesCount()
        {
            return _enemyCounter;
        }

        public void AmountOfEnemiesDestroyed()
        {
            _enemyCounter++;
            Debug.Log("AMOUNT OF ENEMIES OF ACTUAL WAVE: " + _enemyCounter);
        }


    }
}

