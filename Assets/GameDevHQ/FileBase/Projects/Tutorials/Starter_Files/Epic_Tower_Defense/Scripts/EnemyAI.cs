using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Managers;
using GameDevHQ.FileBase.Missle_Launcher;

namespace Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        public Transform _destination;
        [SerializeField]
        private float _hitPoints = 10;
        [SerializeField]
        private float _moneyLoot;
        [SerializeField]
        private bool _enemyState = true;

        //Receiving Damage
        [SerializeField]
        private BoxCollider _boxCollider;


        // Start is called before the first frame update
        void OnEnable()
        {
            Missle_Launcher.ReturnEnemyStatus = IsEnemyActive;

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

        private void Update()
        {
            DestroyEnemy();
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

        //FOR COLLISION WITH MISSILES OR TO TAKE DAMAGE
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                Debug.Log("MISSILE DAMAGE DONE");
                _hitPoints -= 5;

            }
        }

        private void DestroyEnemy()
        {
            if (_hitPoints <= 0)
            {
                this.gameObject.SetActive(false);
                _enemyState = false;
            }
            
        }

        public bool IsEnemyActive()
        {
            return _enemyState;
        }


    }
}

