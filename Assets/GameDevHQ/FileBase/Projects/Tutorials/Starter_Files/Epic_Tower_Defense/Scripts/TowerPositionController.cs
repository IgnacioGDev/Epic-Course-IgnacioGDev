using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

namespace Scripts
{
    public class TowerPositionController : MonoBehaviour
    {

        [SerializeField]
        private MeshRenderer[] _mRenders;
        private bool _isAvailable = true;
        private Color _greenColor;

        // Start is called before the first frame update
        void Start()
        {
            _mRenders = GetComponentsInChildren<MeshRenderer>();
            _greenColor = new Color(0.4f, 1f, 0f, 0.15f);

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
                foreach (var mR in _mRenders)
                {
                    mR.enabled = true;

                    //if (mR.gameObject.name == "RadiusCoverage")
                    //{
                    //    mR.material.color = _greenColor;
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

        void CancelTowerPlacement()
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                foreach (var mR in _mRenders)
                {
                    mR.enabled = false;
                }
            }
        }
    }
}

