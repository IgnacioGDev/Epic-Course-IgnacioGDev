using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UpperBodyMech : MonoBehaviour
{
    [SerializeField]
    private Component[] _components;
    public ParentConstraint _test;


    public static event Func<float> OnMechDestroyed;

    private void Start()
    {
        _components = GetComponents<Component>();
        _test = GetComponent<ParentConstraint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Attack Radius")
        {
            _test.constraintActive = true;
        }
            
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.name == "Attack Radius")
        //{
        //    if (OnMechDestroyed != null)
        //    {
        //        if (OnMechDestroyed() <= 0)
        //        {
        //            _test.constraintActive = false;
        //        }
        //    }
        //}

    }

}
