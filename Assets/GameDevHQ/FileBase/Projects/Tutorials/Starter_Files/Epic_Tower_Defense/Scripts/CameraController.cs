using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Scripts
{
    public class CameraController : MonoBehaviour
    {

        private float _horizontalInput;
        private float _verticalInput;
        private float _mouseWheelInput;
        [SerializeField]
        private float _speedMovement;
        [SerializeField]
        private float scale;
        [SerializeField]
        private float _xBoundaryMin = -20f, _xBoundaryMax = 10f;
        [SerializeField]
        private float _yBoundaryMin = -7f, _yBoundaryMax = 7.5f;
        [SerializeField]
        private float _zBoundaryMin = -7f, _zBoundaryMax = 7f;
        [SerializeField]
        private float _borderThickness = 10f;
        [SerializeField]
        private float _panSpeed;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CameraMovement();
            CamMouseMovement();
        }

        void CameraMovement()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _mouseWheelInput = Input.mouseScrollDelta.y * scale;

            var move = new Vector3(_horizontalInput, _mouseWheelInput, _verticalInput) * _speedMovement * Time.deltaTime;
            transform.Translate(move);

            //Clamps the values of x and z axis based on min and max values given below
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _xBoundaryMin, _xBoundaryMax),
                Mathf.Clamp(transform.position.y, _yBoundaryMin, _yBoundaryMax),
                Mathf.Clamp(transform.position.z, _zBoundaryMin, _zBoundaryMax));
        }

        private void CamMouseMovement()
        {
            //Captures the position of the camera and assigns it to 'pos' variable
            var pos = transform.position;

            /*checks if mouse y axis is less than the height of the screen. If so,
              then adds the 'panSpeed' * Time.deltaTime to the pos.x value every time the condition has been meet */
            if (Input.mousePosition.y >= Screen.height)
            {
                pos.x += _panSpeed * Time.deltaTime;
                //transform.Translate(Vector3.forward * _panSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.y <= _borderThickness)
            {
                pos.x -= _panSpeed * Time.deltaTime;
                //transform.Translate(Vector3.back * _panSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.x >= Screen.width)
            {
                pos.z -= _panSpeed * Time.deltaTime; ;
                //transform.Translate(Vector3.right * _panSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.x <= _borderThickness)
            {
                pos.z += _panSpeed * Time.deltaTime;
                //transform.Translate(Vector3.left * _panSpeed * Time.deltaTime);
            }
            /* 'pos' value (and its axles) are assigned back to camera's transform.position,
             * thus, the camera position.x and z values will change according the if statements above */
            transform.position = pos;
        }
    }
}

