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
        private Vector3 _enemyPos;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _isEnemyInRange = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            _enemy = GameObject.FindGameObjectWithTag("Enemy"); 
            _enemyPos = _enemy.transform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
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
