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
        public Transform _destination;
        [SerializeField]
        private float _hitPoints;
        [SerializeField]
        private float _moneyLoot;


        // Start is called before the first frame update
        void OnEnable()
        {
            if (_navMeshAgent != null)
            {
                //_navMeshAgent.enabled = true;
                _navMeshAgent.Warp(SpawnManager_ScriptableObjects.Instance.GetStartPos());
                _destination = GameManager.Instance.GetEndZone();
                EnemyDestination(_destination.transform.position);
            }
            else
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();

                if (_navMeshAgent != null)
                {
                    _navMeshAgent.Warp(SpawnManager_ScriptableObjects.Instance.GetStartPos());
                    _destination = GameManager.Instance.GetEndZone();
                    EnemyDestination(_destination.transform.position);
                } 
            }
        }   

        public void EnemyDestination(Vector3 endPoint)
        {

            if (_navMeshAgent != null)
            {
                Move();
            }
        }

        void Move()
        {
            _navMeshAgent.SetDestination(_destination.transform.position);
        }

        private void OnDestroy()
        {
            //SpawnManager.Instance._enemyCounter--;
        }

        public Vector3 SetDestination()
        {
            _destination = GameManager.Instance.GetEndZone();
            return _destination.transform.position;
        }
    }
}

