using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.FileBase.Missle_Launcher.Missle;
using Scripts;

namespace GameDevHQ.FileBase.Missle_Launcher
{
    public class Missle_Launcher : MonoBehaviour, IAttackable
    {
        [SerializeField]
        private GameObject _missilePrefab; //holds the missle gameobject to clone
        [SerializeField]
        private GameObject[] _misslePositions; //array to hold the rocket positions on the turret
        [SerializeField]
        private float _fireDelay; //fire delay between rockets
        [SerializeField]
        private float _launchSpeed; //initial launch speed of the rocket
        [SerializeField]
        private float _power; //power to apply to the force of the rocket
        [SerializeField]
        private float _fuseDelay; //fuse delay before the rocket launches
        [SerializeField]
        private float _reloadTime; //time in between reloading the rockets
        [SerializeField]
        private float _destroyTime = 10.0f; //how long till the rockets get cleaned up
        private bool _launched; //bool to check if we launched the rockets

        //ADDED BY ME
        [SerializeField]
        private AttackRadius _attackRadius;
        [SerializeField]
        private GameObject _missileSupport;

        public static Func<bool> ReturnEnemyStatus;

        private void OnEnable()
        {

        }

        private void Update()
        {
            //_attackRadius.QueueNumber();
            Attack();
        }

        IEnumerator FireRocketsRoutine()
        {
            for (int i = 0; i < _misslePositions.Length; i++) //for loop to iterate through each missle position
            {
                if (ReturnEnemyStatus() == true)
                {
                    GameObject rocket = Instantiate(_missilePrefab) as GameObject; //instantiate a rocket

                    rocket.transform.parent = _misslePositions[i].transform; //set the rockets parent to the missle launch position 
                    rocket.transform.localPosition = Vector3.zero; //set the rocket position values to zero
                    rocket.transform.localEulerAngles = new Vector3(-90, 0, 0); //set the rotation values to be properly aligned with the rockets forward direction
                    rocket.transform.parent = null; //set the rocket parent to null

                    if (rocket.GetComponent<Missle.Missle>() != null)
                    {
                        rocket.GetComponent<Missle.Missle>().AssignMissleRules(_launchSpeed, _power, _fuseDelay, _destroyTime); //assign missle properties 
                    }
                    

                    _misslePositions[i].SetActive(false); //turn off the rocket sitting in the turret to make it look like it fired

                    yield return new WaitForSeconds(_fireDelay); //wait for the firedelay

                }

                
            }

            for (int i = 0; i < _misslePositions.Length; i++) //itterate through missle positions
            {
                yield return new WaitForSeconds(_reloadTime); //wait for reload time
                _misslePositions[i].SetActive(true); //enable fake rocket to show ready to fire
            }

            _launched = false; //set launch bool to false
        }

        public void Attack()
        {
            if (ReturnEnemyStatus != null)
            {
                if (_attackRadius.IsRadiusActive() == true && ReturnEnemyStatus() == true)
                {
                    Vector3 direction = _attackRadius.GetEnemyPosition() - transform.position;
                    _missileSupport.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

                    if (_launched == false) //check for space key and if we launched the rockets
                    {

                        _launched = true; //set the launch bool to true
                        StartCoroutine(FireRocketsRoutine()); //start a coroutine that fires the rockets. 
                    }
                }
            }
        }
    }
}

