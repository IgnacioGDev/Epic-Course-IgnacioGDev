using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //if (TowerPlacementController.Instance.GetIsTowerButtonBeingPushed() == false)
            //{
            //    mR.enabled = false;
            //}
        }
    }
}
