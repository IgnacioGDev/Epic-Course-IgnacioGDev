using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scripts.Managers;
using GameDevHQ.FileBase.Missle_Launcher;
using System;
using GameDevHQ.FileBase.Gatling_Gun;
using GameDevHQ.FileBase.Dual_Gatling_Gun;
using UnityEngine.Animations;
using UnityEngine.UI;

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
        private int _moneyLoot;
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
        [SerializeField]
        private bool _lootMoneyToogle;

        //[SerializeField]
        //private GameObject _sparks;
        //[SerializeField]
        //private ParticleSystem _spksParticle;

        //Receiving Damage
        [SerializeField]
        private BoxCollider _boxCollider;

        public static event Action<GameObject> onDeath;
        public static event Action onCheckingEnemiesDestroyed;

        [SerializeField]
        private Transform _targetToAttack;
        [SerializeField]
        private Transform _upperBody;

        //Dissolve Shader
        private Renderer[] _renderers;

        //Variables for the healthbar
        [SerializeField]
        private Image _healthBar;
        [SerializeField]
        private float _health;

        //OPTIMIZATION
        private WaitForSeconds _deathEffectDelayYield = new WaitForSeconds(2f);
        private WaitForSeconds _disableEnemyYield = new WaitForSeconds(5f);

        // Start is called before the first frame update
        void OnEnable()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _renderers = GetComponentsInChildren<Renderer>();

            _lootMoneyToogle = true;

            _health = _hitPoints;

            Missle_Launcher.ReturnEnemyStatus = IsEnemyActive;
            Gatling_Gun.ReturnEnemyStatus = IsEnemyActive;
            Dual_Gatling_Gun.ReturnEnemyStatus = IsEnemyActive;
            GameDevHQ.FileBase.Missle_Launcher_Dual_Turret.Missle_Launcher.ReturnEnemyStatus = IsEnemyActive;
            AttackRadius.onGatlingGunDamage += GatlingGunDamage;




            //AttackRadius.onGatlingGunFX += GatlingGunFX;s

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
            Debugging();
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
            if (other.name == "Attack Radius")
            {
                _animator.SetBool("isAttacking", true);
            }
        }

        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.name == "Attack Radius")
        //    {
        //        _targetToAttack = other.transform;
        //        Debug.Log("INSIDE TOWER");
        //        var direction = other.transform.position - transform.position;
        //        transform.rotation = Quaternion.LookRotation(direction);
        //    }
        //}

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Attack Radius")
            {
                _animator.SetBool("isAttacking", false);
                //_upperBodyMech.transform.Rotate(Vector3.forward, 0f);
            }
        }

        private void UpdateHealthBar()
        {
            var calc = _hitPoints / _health;
            _healthBar.fillAmount = calc;
        }

        private void DestroyEnemy()
        {
            if (_isAlive == true)
            {
                if (_hitPoints <= 0)
                {
                    _hitPoints = 0;
                    UpperBodyMech.OnMechDestroyed += GetHitPoints;
                    //GameManager.OnAddLootedFunds += GetMoneyLooted;
                    if (_lootMoneyToogle == true)
                    {
                        _lootMoneyToogle = false;
                        GameManager.Instance.AddLootedFunds(GetMoneyLooted());  
                    }
                    


                    var parentConstraint = _upperBody.GetComponent<ParentConstraint>();
                    parentConstraint.constraintActive = false;

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
                    _animator.SetBool("isAttacking", false);
                    StartCoroutine(DeathEffectDelay());
                }
            }
        }

        IEnumerator DeathEffectDelay()
        {
            //yield return new WaitForSeconds(2f);
            yield return _deathEffectDelayYield;
            StartCoroutine(DissolveEffect());
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
            UpdateHealthBar();

        }

        public float GetHitPoints()
        {
            return _hitPoints;
        }

        IEnumerator DisableEnemy()
        {
            //yield return new WaitForSeconds(5f);
            yield return _disableEnemyYield;
            this.gameObject.SetActive(false);
        }

        IEnumerator DissolveEffect()
        {
            var fill = 0f;

            while (fill < 1)
            {
                fill += Time.deltaTime / 2;
                foreach (var rend in _renderers)
                {
                    rend.material.SetFloat("_fillAmount", fill);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private int GetMoneyLooted()
        {
            if (gameObject.name == "Mech1(Clone)")
            {
                return _moneyLoot = 150;
            }
            if (gameObject.name == "Mech2(Clone)")
            {
                return _moneyLoot = 250;
            }
            else
            {
                return 0;
            }

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

        //Debug
        private void Debugging()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _hitPoints = 0f;
            }
        }

    }
}

