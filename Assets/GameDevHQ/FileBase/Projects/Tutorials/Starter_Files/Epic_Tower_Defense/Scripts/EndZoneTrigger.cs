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
            Destroy(other.gameObject);
        }

        private void OnMouseEnter()
        {
            Debug.Log("MOUSE ENTERED");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("ENEMY COLLIDED");
        }
    }

}

