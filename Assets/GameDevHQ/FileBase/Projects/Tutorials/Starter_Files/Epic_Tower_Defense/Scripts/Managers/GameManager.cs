using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

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
        private float _moneyCounter;

        //Singleton instantiation
        private void Awake()
        {
            _instance = this;
        }

        public Transform GetEndZone()
        {
            return _endZoneTrigger;
        }
    }

}
