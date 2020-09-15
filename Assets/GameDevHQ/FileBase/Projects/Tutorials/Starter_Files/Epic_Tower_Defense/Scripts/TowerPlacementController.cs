using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject _fakeTower;
    [SerializeField]
    private GameObject _gatlingGun;
    private bool _canPlaceTower = false;
    private MeshRenderer[] _childMeshRenderers;


    // Start is called before the first frame update
    void Start()
    {
        _fakeTower.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        TowerUnderMouseMovement();
        Debug.Log(_canPlaceTower);

    }

    private void TowerUnderMouseMovement()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Raycast info/ hitting: " + hitInfo.collider);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _fakeTower.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                _fakeTower.SetActive(false);
            }
            _fakeTower.transform.position = hitInfo.point;
            //hit info may contain a valid spot
            //valid spot is true?
            //_canPlaceTower = true;

            if (_canPlaceTower == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(_gatlingGun, hitInfo.point, Quaternion.identity);
                    _canPlaceTower = false;
                    hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }

            //EXPERIMENTAL!!!
            if (hitInfo.collider.gameObject.name == "TowerPosition")
            {
                _canPlaceTower = true;
                _childMeshRenderers = hitInfo.collider.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (var mR in _childMeshRenderers)
                {
                    //mR.enabled = true;
                }

            }
            else
            {
                _canPlaceTower = false;

            }
        }       
    }
}
