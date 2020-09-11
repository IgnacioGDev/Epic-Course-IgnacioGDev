using Scripts;
using Scripts.Managers;
using Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager _instance;
        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("The Pool Manager is NULL");
                }
                return _instance;
            }
        }

        [SerializeField]
        private GameObject _enemy;
        [SerializeField]
        private List<GameObject> _enemyPool;
        [SerializeField]
        private GameObject _enemyContainer;
        [SerializeField]
        private List<Wave> _waveList;
        [SerializeField]
        private int _randomIndex;
        private int _minIndex = 0;

        [SerializeField]
        private int _indexCounter = 0;


        [SerializeField]
        private int _currentWaveIndex = 0;

        private void Awake()
        {
            _instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            GenerateEnemies();
        }

        private void  GenerateEnemies()
        {

                foreach (Wave wave in _waveList)
                {
                    //Debug.Log("NUMBER OF WAVES: " + _waveList.Count);
                    //create a container for each wave right here
                    var nextWave = new GameObject("Wave: " + wave.waveID);
                    nextWave.transform.parent = _enemyContainer.transform;
                    
                    foreach (GameObject enemy in wave.enemies)
                    {
                        _enemy = Instantiate(enemy, SpawnManager_ScriptableObjects.Instance.GetStartPos(), SpawnManager_ScriptableObjects.Instance.InitRotation());

                        //setting the enemy prefab parent in hierarchy
                        _enemy.transform.parent = nextWave.transform; //
                        _enemy.SetActive(false);

                    }
                }
        }

        public GameObject RequestEnemy()
        {
            //get wave based on currentWaveIndex
            var currentWave = _enemyContainer.transform.GetChild(_currentWaveIndex);
            var currentChildren = currentWave.GetComponentsInChildren<Transform>(true);
            foreach (var enemy in currentChildren)
            {
                if (enemy.gameObject.activeInHierarchy == false)
                {
                    return enemy.gameObject;
                }
            }

            //if we make it here...we need to prepare for next wave
            _currentWaveIndex++;
            return null;
        }

        public List<GameObject> EnemiesInPool()
        {
            return _enemyPool;
        }

        public int ReturnCurrentWaveDelay()
        {
            if (_currentWaveIndex < _waveList.Count)
            {
                //Debug.Log("CURRENT WAVE " + _currentWaveIndex);
                return _waveList[_currentWaveIndex].spawnDelay;
            }
            else
                return 200;
            
        }

        public int GetCurrentWaveCount()
        {
            return _waveList[_currentWaveIndex-1].enemies.Count;
        }
    }
}

