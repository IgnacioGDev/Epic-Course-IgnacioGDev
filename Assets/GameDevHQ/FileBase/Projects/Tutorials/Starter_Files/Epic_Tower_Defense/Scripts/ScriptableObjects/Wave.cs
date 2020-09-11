using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "wave.asset", menuName = "ScriptableObjects/Waves", order = 1)]
    public class Wave : ScriptableObject
    {
        public int waveID;
        public List<GameObject> enemies;
        public int spawnDelay;
    }
}

