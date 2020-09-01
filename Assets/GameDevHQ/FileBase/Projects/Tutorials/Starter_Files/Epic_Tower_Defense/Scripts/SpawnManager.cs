using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabEnemy;
    [SerializeField]
    private Transform _startingPoint;

    //Singleton atribute 
    private static SpawnManager _instance;

    //Singleton property
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
    //Singleton instantiation
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingPoint = GameObject.Find("StartingPoint").GetComponent<Transform>();
        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Instantiate(_prefabEnemy, _startingPoint.transform.position, _startingPoint.transform.rotation);
        }
        
    }
}
