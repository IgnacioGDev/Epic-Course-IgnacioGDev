using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTower : MonoBehaviour
{
    [SerializeField]
    private Transform _targetToAttack;
    [SerializeField]
    private Transform _mechLowerBody;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Attack Radius")
        {
            _targetToAttack = other.transform;
            var direction = other.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.rotation = _mechLowerBody.rotation;
    }
}
