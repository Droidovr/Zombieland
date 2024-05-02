using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NpcSpawnJSONCreator : MonoBehaviour
    {
        public List<NpcSpawnData> GetNpcSpawnDataList()
        {
            var npcSpawnData = new List<NpcSpawnData>();
            var npcSpawnJsonData = GetComponentsInChildren<NpcSpawnJSONData>();
            foreach (var jsonData in npcSpawnJsonData)
            {
                var spawnData = new NpcSpawnData(jsonData.npcJsonFileName, jsonData.SpawnPositionTransform, jsonData.wanderPositionTransforms);
                npcSpawnData.Add(spawnData);
            }
            return npcSpawnData;
        }
    }
}
