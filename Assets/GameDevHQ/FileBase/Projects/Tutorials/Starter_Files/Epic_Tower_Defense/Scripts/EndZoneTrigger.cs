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
        public static event Action onWaveDestroyed;

        private void OnEnable()
        {
            EnemyAI.onCheckingEnemiesDestroyed += CheckingEnemiesInWave;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ENEMY DESTROYED");

            other.gameObject.SetActive(false);

            if (onEnemyReachedEnd != null)
            {
                onEnemyReachedEnd();
            }

            //Check if next wave should start
            /*Comparar nu mero de objetos eliminados de la wave actual con numero de enemigos experados de wave actual
             por ejemplo
            (if numeroDeEnemigosEnPantallaDestruidos == numeroDeEnemigosWaveActual)
            {
                Llamar corrutina para empezar siguente wave;
            }*/
            SpawnManager_ScriptableObjects.Instance.AmountOfEnemiesDestroyed();

            if (SpawnManager_ScriptableObjects.Instance.GetCurrentEnemiesCount() >= PoolManager.Instance.GetCurrentWaveCount())
            {
                Debug.Log("WAVE FINISHED, INITIATING NEXT WAVE");
                SpawnManager_ScriptableObjects.Instance.StartNextWave();
            }
            else
            {
                Debug.Log("WAVE YET TO BE FNISHED");
            }

        }

        private void CheckingEnemiesInWave()
        {
            if (SpawnManager_ScriptableObjects.Instance.GetCurrentEnemiesCount() >= PoolManager.Instance.GetCurrentWaveCount())
            {
                if (onWaveDestroyed != null)
                {
                    onWaveDestroyed();
                }
                Debug.Log("WAVE FINISHED, INITIATING NEXT WAVE");
                SpawnManager_ScriptableObjects.Instance.StartNextWave();
            }
            else
            {
                Debug.Log("WAVE YET TO BE FNISHED");
            }
        }
    }

}

