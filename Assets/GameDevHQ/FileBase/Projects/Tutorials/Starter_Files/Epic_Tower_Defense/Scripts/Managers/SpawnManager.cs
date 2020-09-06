using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        // Singleton atribute 
        private static SpawnManager _instance;

        // Singleton property
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("The Spawn Manager is NULL");
                }
                return _instance;
            }
        }

        [SerializeField]
        private GameObject[] _prefabEnemies;
        [SerializeField]
        private Transform _startingPoint;
        [SerializeField]
        private int _initialWave = 10;
        [SerializeField]
        private int _actualWave;
        [SerializeField]
        private int _waveMultiplier = 1;
        private int _randomIndexEnemy;
        private float _randomIndexMin = 0f;
        private float _randomIndexMax = 1f;
        public int _enemyCounter;

        // Singleton instantiation
        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (_startingPoint == null)
            {
                Debug.LogError("STARTING POINT IS NULL");
            }

            _enemyCounter = _initialWave * _waveMultiplier;
            StartCoroutine(EnemySpawner());

        }

        private void Update()
        {
           ResetEnemyCounter();
        }

        IEnumerator EnemySpawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (_actualWave < (_initialWave * _waveMultiplier))
                {

                    //communicate with the object pool system
                    //Request enemy
                    GameObject enemy = PoolManager.Instance.RequestEnemy();
                    enemy.SetActive(true);
                    //enemy.transform.position = _startingPoint.transform.position;
                    

                    Debug.Log("ENEMY SPAWNED");

                    //Instantiate(_prefabEnemies[RandomIndexGenerator()], _startingPoint.transform.position, _startingPoint.transform.rotation);
                    Debug.Log(_randomIndexEnemy);
                    _actualWave++;
                }
            }

        }

        private int RandomIndexGenerator()
        {
            return _randomIndexEnemy = Mathf.RoundToInt(Random.Range(_randomIndexMin, _randomIndexMax));
        }

        void ResetEnemyCounter()
        {
            if (_enemyCounter == 0)
            {
                _actualWave = 0;
                _waveMultiplier++;
                _enemyCounter = _waveMultiplier * _initialWave;
            }
        }

        public Vector3 GetStartPos()
        {
            return _startingPoint.transform.position;
        }
    }
}

