using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private Transform _destination;
    [SerializeField]
    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _destination = GameObject.Find("EndPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_destination.transform.position);
        EnemyDestination(_destination.transform.position);
    }

    void EnemyDestination(Vector3 endPoint)
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.SetDestination(endPoint);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Endpoint"))
        {
            Destroy(gameObject);
        }
    }
}
