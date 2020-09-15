using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

public class TowerPositionController : MonoBehaviour
{
    private MeshRenderer[] _mRenders;
    // Start is called before the first frame update
    void Start()
    {
        _mRenders = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    private void OnMouseEnter()
    {
        if (TowerPlacementController.Instance.GetIsTowerButtonBeingPushed() == true)
        {
            foreach (var mR in _mRenders)
            {
                mR.enabled = true;
                //if (TowerPlacementController.Instance.GetIsTowerButtonBeingPushed() == true)
                //{
                //    mR.enabled = true;
                //}         
            }
        }
    }

    private void OnMouseExit()
    {
        foreach (var mR in _mRenders)
        {
            mR.enabled = false;
        }
    }

    void Test()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var mR in _mRenders)
            {
                mR.enabled = false;
            }
        }
    }

    
}
