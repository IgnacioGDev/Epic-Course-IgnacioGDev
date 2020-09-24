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
        private GameObject _enemy;
        [SerializeField]
        private int _queueIndex = 0;
        private Vector3 _enemyPos;

        //QUEUE
        [SerializeField]
        private List<GameObject> _enemiesInQueue;

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
            if (other.CompareTag("Enemy"))
            {
                _isEnemyInRange = true;
                _enemyPos = _enemiesInQueue[_queueIndex].transform.position;
                //_enemy = GameObject.FindGameObjectWithTag("Enemy");
                //_enemyPos = _enemy.transform.position;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //_enemiesInQueue.Remove(other.gameObject);
                _queueIndex++;
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
    }

}
