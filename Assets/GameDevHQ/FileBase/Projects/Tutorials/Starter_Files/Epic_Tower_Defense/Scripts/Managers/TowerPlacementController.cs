using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
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
        [SerializeField]
        private GameObject _missileLauncher;
        [SerializeField]
        private GameObject _fakeMissilieLauncher;
        [SerializeField]
        private GameObject _radius;
        private bool _canPlaceTower = false;
        private MeshRenderer[] _childMeshRenderers;
        private bool _isHotKeyPushed = false;

        private bool _isTowerSelected = false;
        private bool _isMissileSelected = false;


        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _fakeTower.SetActive(false);
            _radius.SetActive(false);
            _fakeMissilieLauncher.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            TowerUnderMouseMovement();


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
                    _isTowerSelected = true;
                    _isMissileSelected = false;
                    _fakeTower.SetActive(true);
                    _radius.SetActive(true);
                    _isHotKeyPushed = true;
                    _radius.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && _fakeMissilieLauncher.activeSelf == false)
                {
                    _isMissileSelected = true;
                    _isTowerSelected = false;
                    _fakeMissilieLauncher.SetActive(true);
                    _radius.SetActive(true);
                    _isHotKeyPushed = true;
                    _radius.GetComponent<MeshRenderer>().material.color = Color.red;
                }

                //FakeTower follows Mouse position
                _fakeTower.transform.position = hitInfo.point;
                _fakeMissilieLauncher.transform.position = hitInfo.point;
                _radius.transform.position = hitInfo.point;

                //hitInfo checks if mouse is hovering over "TowerPosition"
                if (hitInfo.collider.gameObject.name == "TowerPosition")
                {
                    _canPlaceTower = true;
                    _fakeTower.SetActive(false);
                    _fakeMissilieLauncher.SetActive(false);
                    _radius.GetComponent<MeshRenderer>().material.color = Color.green;
                    _radius.SetActive(false);
                }
                else
                {
                    _canPlaceTower = false;
                    if (_isHotKeyPushed == true)
                    {
                        if (_isTowerSelected == true)
                        {
                            _fakeTower.SetActive(true);
                            _radius.SetActive(true);
                            _radius.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        else if (_isMissileSelected == true)
                        {
                            _fakeMissilieLauncher.SetActive(true);
                            _radius.SetActive(true);
                            _radius.GetComponent<MeshRenderer>().material.color = Color.red;
                        }

                    }
                    
                }

                if (_canPlaceTower == false)
                {
                    if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                    {
                        _isHotKeyPushed = false;
                        _fakeTower.SetActive(false);
                        _fakeMissilieLauncher.SetActive(false);
                        _radius.SetActive(false);
                    }
                }

                //TOWER LOGIC INSTANTIATION
                if (_canPlaceTower == true)
                {
                    if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                    {
                        _isHotKeyPushed = false;
                        _fakeTower.SetActive(false);
                        _fakeMissilieLauncher.SetActive(false);
                        _radius.SetActive(false);
                    }

                    if (Input.GetMouseButtonDown(0) && _isHotKeyPushed == true)
                    {
                        //FIX THIS CRAP AS SOON AS YOU HAVE TIME
                        if (GameManager.Instance.GetWarFunds() > 350f)
                        {
                            if (_isTowerSelected == true)
                            {
                                Instantiate(_gatlingGun, hitInfo.collider.transform.position, Quaternion.identity);
                                _canPlaceTower = false;
                                hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                                /*Invokes TowerPlaced() method from Particles class, which checks if _isAvailable is true or false.
                                 if is false then turns of the particles for this spot in the Particles script */
                                hitInfo.collider.gameObject.GetComponent<Particles>().TowerPlaced();
                                GameManager.Instance.ChargeWarFunds();
                            }
                            else if (_isMissileSelected == true)
                            {
                                Instantiate(_missileLauncher, hitInfo.collider.transform.position, Quaternion.identity);
                                _canPlaceTower = false;
                                hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                                /*Invokes TowerPlaced() method from Particles class, which checks if _isAvailable is true or false.
                                 if is false then turns of the particles for this spot in the Particles script */
                                hitInfo.collider.gameObject.GetComponent<Particles>().TowerPlaced();
                                GameManager.Instance.ChargeWarFunds();
                            }
                            
                        }

                    }

                }

            }
        }

        public bool GetIsTowerButtonBeingPushed()
        {
            return _isHotKeyPushed;
        }

        public bool IsTowerSelected()
        {
            return _isTowerSelected;
        }

        public bool IsMissileSelected()
        {
            return _isMissileSelected;
        }

    }

}
