using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class EndZoneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }

}

