using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts
{
    public class EndZoneTrigger : MonoBehaviour
    {
        public static event Action onEnemyReachedEnd; 

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ENEMY DESTROYED");
            other.gameObject.SetActive(false);
            //SpawnManager.Instance.OnEndZoneReach();
            //SpawnManager.Instance._enemyCounter--;
            if (onEnemyReachedEnd != null)
            {
                onEnemyReachedEnd();
            }
            
        }
    }

}

