using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NpcSpawnJSONData : MonoBehaviour
    {
        public string npcJsonFileName; // Used for Load NpcData JSON
        public Transform SpawnPositionTransform => transform;
        public List<Transform> wanderPositionTransforms;
    }
}
