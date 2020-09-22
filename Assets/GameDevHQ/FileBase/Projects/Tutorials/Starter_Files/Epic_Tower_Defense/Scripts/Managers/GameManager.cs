using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        //Singleton atribute
        private static GameManager _instance;

        //Singleton property
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GAME MANAGER CANNOT BE EMPTY!!");
                }
                return _instance;
            }
        }

        [SerializeField]
        private Transform _endZoneTrigger;
        [SerializeField]
        private Text _inGameFunds;
        private float _warFunds;


        //Singleton instantiation
        private void Awake()
        {
            _instance = this;
            
        }

        private void Start()
        {
            _warFunds = 1000;
        }

        private void OnEnable()
        {
            TowerPositionController.onBuyingTower += ChargeWarFunds;
        }

        private void Update()
        {
            _inGameFunds.text = _warFunds.ToString();
        }

        public Transform GetEndZone()
        {
            return _endZoneTrigger;
        }

        public int GetWarFunds()
        {
            return Mathf.RoundToInt(_warFunds);
        }

        public void ChargeWarFunds()
        {
            if (_warFunds > 0)
            {
                _warFunds -= 250;
            }
            else
            {
                Debug.Log("NOT ENOUGH WAR FUNDS");
            }
            

        }

        private void OnDisable()
        {
            TowerPositionController.onBuyingTower -= ChargeWarFunds;
        }




    }

}
