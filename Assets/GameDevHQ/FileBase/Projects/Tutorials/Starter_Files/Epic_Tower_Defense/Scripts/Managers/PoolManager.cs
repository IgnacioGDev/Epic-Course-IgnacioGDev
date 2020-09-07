using Scripts;
using Scripts.Managers;
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
        private GameObject[] _enemyPrefabs;
        [SerializeField]
        private List<GameObject> _enemyPool;
        [SerializeField]
        private GameObject _enemyContainer;
        [SerializeField]
        private int _randomIndex;
        private int _minIndex = 0;

        private void Awake()
        {
            _instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            _enemyPool = GenerateEnemies(3);
        }

        List<GameObject> GenerateEnemies(int amountOfEnemies)
        {
            for (int i = 0; i < amountOfEnemies; i++)
            {

                //GameObject enemy = Instantiate(_enemyPrefab);
                GameObject enemy = Instantiate(_enemyPrefabs[RandomIndexGenerator()]);
                
                //setting the enemy prefab parent in hierarchy
                enemy.transform.parent = _enemyContainer.transform;
                enemy.SetActive(false);
                _enemyPool.Add(enemy);
            }
            return _enemyPool;
        }
        private int RandomIndexGenerator()
        {
            return _randomIndex = Mathf.RoundToInt(Random.Range(_minIndex, _enemyPrefabs.Length));
        }

        public GameObject RequestEnemy()
        {
            /*loop through the enemy list and will return the first enemy
            that is not active in hierarchy (activating it in the process) */
            foreach (var enemy in _enemyPool)
            {
                if (enemy.activeInHierarchy == false)
                {
                    //enemy available
                    enemy.SetActive(true);
                    //gives enemy to spawn manager
                    return enemy;
                }
                else if (enemy == null)
                {
                    Debug.Log("RequestEnemy()::No more objects available to request!");
                }
            }

            /*if inactive enemies were not found in the hierarchy (through the
              for loop above), then new ones are going to be instantiated */
            GameObject newEnemy = Instantiate(_enemyPrefabs[RandomIndexGenerator()], SpawnManager.Instance.GetStartPos(), transform.rotation);
            Debug.Log(RandomIndexGenerator());
            newEnemy.transform.parent = _enemyContainer.transform;
            /*Line 72 sets the newEnemy destination atribute, accessing and setting it through GetComponent<EnemyAI> Class, 
              then saving the value into the "endPoint" variable. */
            newEnemy.GetComponent<EnemyAI>()._destination = GameManager.Instance.GetEndZone();
            var endPoint = newEnemy.GetComponent<EnemyAI>()._destination;
            newEnemy.GetComponent<EnemyAI>().EnemyDestination(endPoint.transform.position);
            _enemyPool.Add(newEnemy);

            return newEnemy;
        }
    }
}

