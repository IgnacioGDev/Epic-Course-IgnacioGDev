using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Managers;
using GameDevHQ.FileBase.Missle_Launcher;
using System;
using GameDevHQ.FileBase.Gatling_Gun;

namespace Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        public Transform _destination;
        [SerializeField]
        private float _hitPoints = 10;
        [SerializeField]
        private float _moneyLoot;
        [SerializeField]
        private bool _enemyState = true;
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private bool _isAlive = true;

        [SerializeField]
        private GameObject _explotion;
        [SerializeField]
        private ParticleSystem _explotionParticle;

        [SerializeField]
        private GameObject _expMissile;
        [SerializeField]
        private ParticleSystem _expMissilParticle;

        //[SerializeField]
        //private GameObject _sparks;
        //[SerializeField]
        //private ParticleSystem _spksParticle;

        //Receiving Damage
        [SerializeField]
        private BoxCollider _boxCollider;

        public static event Action<GameObject> onDeath;
        public static event Action onCheckingEnemiesDestroyed;


        // Start is called before the first frame update
        void OnEnable()
        {
            _boxCollider = GetComponent<BoxCollider>();


            Missle_Launcher.ReturnEnemyStatus = IsEnemyActive;
            Gatling_Gun.ReturnEnemyStatus = IsEnemyActive;
            AttackRadius.onGatlingGunDamage += GatlingGunDamage;
            //AttackRadius.onGatlingGunFX += GatlingGunFX;

            _explotionParticle.Stop();
            _expMissilParticle.Stop();
            //_spksParticle.Stop();

            _animator = GetComponent<Animator>();
            _animator.SetBool("isDead", false);

            if (_navMeshAgent != null)
            {
                //_navMeshAgent.enabled = true;
                _navMeshAgent.Warp(SpawnManager_ScriptableObjects.Instance.GetStartPos());
                _destination = GameManager.Instance.GetEndZone();
                EnemyDestination(_destination.transform.position);
            }
            else
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();

                if (_navMeshAgent != null)
                {
                    _navMeshAgent.Warp(SpawnManager_ScriptableObjects.Instance.GetStartPos());
                    _destination = GameManager.Instance.GetEndZone();
                    EnemyDestination(_destination.transform.position);
                }
            }
        }



        private void Update()
        {
            //GatlingGunFX();
            DestroyEnemy();
        }

        public void EnemyDestination(Vector3 endPoint)
        {

            if (_navMeshAgent != null)
            {
                Move();
            }
        }

        void Move()
        {
            _navMeshAgent.SetDestination(_destination.transform.position);
        }

        private void OnDestroy()
        {
            //SpawnManager.Instance._enemyCounter--;
        }

        public Vector3 SetDestination()
        {
            _destination = GameManager.Instance.GetEndZone();
            return _destination.transform.position;
        }

        //FOR COLLISION WITH MISSILES OR TO TAKE DAMAGE
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                _hitPoints -= 5;
                //Instantiate(_expMissile, transform.position, Quaternion.identity);
                //if (_expMissilParticle.isPlaying == false)
                //{
                //    Destroy(_expMissile, 4f);
                //}
                _expMissilParticle.Play();

            }
        }

        private void DestroyEnemy()
        {
            if (_isAlive == true)
            {
                if (_hitPoints <= 0)
                {
                    //var expEffect = (GameObject) Instantiate(_explotion, transform.position + new Vector3(0,2f,0), Quaternion.identity);
                    //if (_explotionParticle.isPlaying == false)
                    //{
                    //    Destroy(expEffect, 4f);
                    //}
                    if (_boxCollider != null)
                    {
                        _boxCollider.enabled = false;
                    }
                    if (_navMeshAgent != null)
                    {
                        _navMeshAgent.enabled = false;
                    }
                    
                    _explotionParticle.Play();

                    _animator.SetBool("isDead", true);

                    if (onDeath != null)
                    {
                        onDeath(this.gameObject);
                    }
                    _enemyState = false;
                    //this.gameObject.SetActive(false);

                    //_navMeshAgent.SetDestination(transform.position);
                    
                    StartCoroutine(DisableEnemy());
                    SpawnManager_ScriptableObjects.Instance.AmountOfEnemiesDestroyed();
                    if (onCheckingEnemiesDestroyed != null)
                    {
                        onCheckingEnemiesDestroyed();
                    }
                    _isAlive = false;
                }
            }



        }

        public bool IsEnemyActive()
        {
            return _enemyState;
        }

        public void GatlingGunDamage(Vector3 pos)
        {
            if (_hitPoints >= 0 && transform.position == pos)
            {
                
                _hitPoints -= 0.5f * Time.deltaTime;
            }

        }

        IEnumerator DisableEnemy()
        {
            yield return new WaitForSeconds(5f);
            this.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Missle_Launcher.ReturnEnemyStatus -= IsEnemyActive;
            AttackRadius.onGatlingGunDamage -= GatlingGunDamage;
        }

        //public void GatlingGunFX()
        //{
        //    if (_spksParticle != null)
        //    {
        //        _spksParticle.Play();
        //    }

        //}

    }
}

