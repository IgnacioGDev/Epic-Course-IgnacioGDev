using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    private static TowerPlacementController _instance;
    public static TowerPlacementController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("TowerPlacementController is NULL");
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject _fakeTower;
    [SerializeField]
    private GameObject _gatlingGun;
    private bool _canPlaceTower = false;
    private MeshRenderer[] _childMeshRenderers;
    private bool _isHotKeyPushed = false;


    private void Awake()
    {
        _instance = this;
    }

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

            if (Input.GetKeyDown(KeyCode.Alpha1) && _fakeTower.activeSelf == false)
            {
                _fakeTower.SetActive(true);
                _isHotKeyPushed = true;
                
            }
            if (_canPlaceTower == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("CLICK ON DIFFERENT TERRAIN");
                    _fakeTower.SetActive(false);

                }
            }

            _fakeTower.transform.position = hitInfo.point;
            //hit info may contain a valid spot
            //valid spot is true?
            //_canPlaceTower = true;

            if (_canPlaceTower == true)
            {
                if (Input.GetMouseButtonDown(0) && _isHotKeyPushed == true)
                {
                    Instantiate(_gatlingGun, hitInfo.collider.transform.position, Quaternion.identity);
                    _canPlaceTower = false;
                    hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                    _isHotKeyPushed = false;
                }
            }
            if (hitInfo.collider.gameObject.name == "TowerPosition")
            {
                _canPlaceTower = true;
                _fakeTower.SetActive(false);
            }
            else
            {
                _canPlaceTower = false;
                if (_isHotKeyPushed == true)
                {
                    _fakeTower.SetActive(true);    
                }
            }
        }
    }

    public bool GetIsTowerButtonBeingPushed()
    {
        return _isHotKeyPushed;
    }
}
