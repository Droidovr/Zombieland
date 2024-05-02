using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule
{
    public class NpcSpawnData : MonoBehaviour
    {
        [SerializeField] private string _npcName;
        [SerializeField] private List<Transform> _wanderPositionTransforms;

        public string NpcName => _npcName; // Used for Load NpcData JSON
        public Transform SpawnPositionTransform => transform;
        public List<Transform> WanderPositionTransforms => _wanderPositionTransforms;
    }
}
