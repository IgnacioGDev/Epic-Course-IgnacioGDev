using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class EndZoneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ENEMY DESTROYED");
            other.gameObject.SetActive(false);
            SpawnManager.Instance._enemyCounter--;
        }
    }

}

