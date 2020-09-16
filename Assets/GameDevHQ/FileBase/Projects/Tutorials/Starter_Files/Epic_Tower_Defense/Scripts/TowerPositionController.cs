using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

public class TowerPositionController : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private MeshRenderer[] _mRenders;
    [SerializeField]
    private GameObject _particle;


    // Start is called before the first frame update
    void Start()
    {
        _mRenders = GetComponentsInChildren<MeshRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particle = GameObject.Find("Hex_Selection");
    }

    // Update is called once per frame
    void Update()
    {
        CancelTowerPlacement();
        ManageParticles();
    }

    private void OnMouseEnter()
    {
        if (TowerPlacementController.Instance.GetIsTowerButtonBeingPushed() == true)
        {
            foreach (var mR in _mRenders)
            {
                mR.enabled = true;
     
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

    void CancelTowerPlacement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var mR in _mRenders)
            {
                mR.enabled = false;
            }
        }
    }

    public void ManageParticles()
    {
        if (TowerPlacementController.Instance.TowerHasBeenPlaced() == true)
        {
            Debug.Log("PARTICLES // TOWER HAS BEEN PLACED");
            _particleSystem.enableEmission = false;
            //_particle.SetActive(false);
        }     
    }

    
}
