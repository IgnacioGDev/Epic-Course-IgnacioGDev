using GameDevHQ.FileBase.Dual_Gatling_Gun;
using GameDevHQ.FileBase.Gatling_Gun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class AttackRadius : MonoBehaviour
    {
        [SerializeField]
        private bool _isEnemyInRange = false;
        [SerializeField]
        private int _queueIndex = 0;
        private Vector3 _enemyPos;

        //QUEUE
        [SerializeField]
        private List<GameObject> _enemiesInQueue;

        public static event Action<Vector3> onGatlingGunDamage;
        public static Func<bool> ReturnEnemyStatus;
        public static event Action onGatlingGunFX;


        private void OnEnable()
        {
            Gatling_Gun.GetEnemiesInQueue += QueueNumber;
            Dual_Gatling_Gun.GetEnemiesInQueue += QueueNumber;
            EnemyAI.onDeath += RemoveEnemy;
            EndZoneTrigger.onWaveDestroyed += ResetRadiusRange;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _isEnemyInRange = true;

                //FOR QUEUE
                _enemiesInQueue.Add(other.gameObject);
                //Debug.Log("Enemy name: " + _enemiesInQueue[0].name);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            QueueNumber();
            if (other.CompareTag("Enemy"))
            {
                //_isEnemyInRange = true;


                if (_enemiesInQueue.Count > 0)
                {
                    _enemyPos = _enemiesInQueue[_queueIndex].transform.position;

                    if (onGatlingGunDamage != null)
                    {
                        onGatlingGunDamage(_enemyPos);
                    }
                    if (onGatlingGunFX != null)
                    {
                        onGatlingGunFX();
                    }

                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemiesInQueue.Remove(other.gameObject);
                _isEnemyInRange = false;
            }
        }

        public bool IsRadiusActive()
        {
            return _isEnemyInRange;
        }

        public Vector3 GetEnemyPosition()
        {
            return _enemyPos;
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemiesInQueue.Remove(enemy);
        }

        public void ResetRadiusRange()
        {
            _isEnemyInRange = false;
        }

        public int QueueNumber()
        {
            return _enemiesInQueue.Count;
        }
    }

}
