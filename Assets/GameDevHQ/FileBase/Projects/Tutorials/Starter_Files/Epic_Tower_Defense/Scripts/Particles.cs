using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Particles : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particles;
        private bool _isAvailable = true;
        // Start is called before the first frame update
        void Start()
        {
            _particles = GetComponentInChildren<ParticleSystem>();
            _particles.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            ParticleManagement();
        }

        void ParticleManagement()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                _particles.Play();
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                _particles.Stop();
            }
            else if (_isAvailable == false)
            {
                _particles.Stop();
            }
        }

        public void TowerPlaced()
        {
            _isAvailable = false;
        }
    }
}
