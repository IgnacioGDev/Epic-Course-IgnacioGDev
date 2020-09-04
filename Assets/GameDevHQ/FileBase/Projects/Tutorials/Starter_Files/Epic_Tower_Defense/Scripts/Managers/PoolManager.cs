using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _enemyPool;
    [SerializeField]
    private GameObject _enemyContainer;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = GenerateEnemies(20);
    }

    List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            //setting the enemy prefab parent in hierarchy
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }
        return _enemyPool;
    }

    public GameObject RequestEnemy()
    {   
        //loop through the bullet list
         foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                //enemy available
                enemy.SetActive(true);
                //gives enemy to spawn manager
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }
}
