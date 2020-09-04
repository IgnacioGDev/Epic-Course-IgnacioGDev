using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Managers;

namespace Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private Transform _destination;
        [SerializeField]
        private float _hitPoints;
        [SerializeField]
        private float _moneyLoot;



        //Game Manager holds the reference for the endpoint -- DONE
        //This class will request the gameManager reference -- DONE


        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _destination = GameManager.Instance._endZoneTrigger;
            EnemyDestination(_destination.transform.position);
            
        }

        // Update is called once per frame

        void EnemyDestination(Vector3 endPoint)
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.SetDestination(endPoint);
            }
        }

        private void OnDestroy()
        {
            SpawnManager.Instance._enemyCounter--;
        }
    }
}

