using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class TowerPositionController : MonoBehaviour
    {

        [SerializeField]
        private MeshRenderer[] _towerRenderers;
        [SerializeField]
        private MeshRenderer[] _missileRenderers;
        [SerializeField]
        private Transform[] _childs;
        //private Color _greenColor;
        [SerializeField]
        private MeshRenderer _greenRadius;

        // Start is called before the first frame update
        void Start()
        {
            _towerRenderers = GetComponentsInChildren<MeshRenderer>();
            //_greenColor = new Color(0.4f, 1f, 0f, 0.15f);
            //_missileRenderers = GameObject.Find("Missile_Launcher_Turret_").GetComponentsInChildren<MeshRenderer>();
            _childs = GetComponentsInChildren<Transform>();
            foreach (var child in _childs)
            {
                if (child.gameObject.name == "Missile_Launcher_Turret_")
                {
                    _missileRenderers = child.GetComponentsInChildren<MeshRenderer>();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            CancelTowerPlacement();
        }

        private void OnMouseEnter()
        {
            if (TowerPlacementController.Instance.GetIsTowerButtonBeingPushed() == true)
            {

                if (TowerPlacementController.Instance.IsTowerSelected() == true)
                {
                    foreach (var tower in _towerRenderers)
                    {
                        tower.enabled = true;
                    }
                    foreach (var missile in _missileRenderers)
                    {
                        missile.enabled = false;
                    }
                }
                if (TowerPlacementController.Instance.IsMissileSelected() == true)
                {
                    _greenRadius.enabled = true;
                    foreach (var missile in _missileRenderers)
                    {
                        missile.enabled = true;
                    }


                }
                
            }
        }

        private void OnMouseExit()
        {
            if (TowerPlacementController.Instance.IsTowerSelected() == true)
            {
                foreach (var mR in _towerRenderers)
                {
                    mR.enabled = false;
                }
            }
            else if (TowerPlacementController.Instance.IsMissileSelected() == true)
            {
                _greenRadius.enabled = false;
                foreach (var mR in _missileRenderers)
                {
                    mR.enabled = false;
                }

            }
            //foreach (var mR in _towerRenderers)
            //{
            //    mR.enabled = false;
            //}
        }

        void CancelTowerPlacement()
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                foreach (var mR in _towerRenderers)
                {
                    mR.enabled = false;
                }
            }
        }
    }
}

