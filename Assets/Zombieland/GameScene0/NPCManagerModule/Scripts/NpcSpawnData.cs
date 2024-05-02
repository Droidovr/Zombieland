using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.NPCManagerModule
{
    [Serializable]
    public class NpcSpawnData
    {
        public string NpcJsonFileName { get; set; } // Used for Load NpcData JSON
        [JsonIgnore] public Vector3 SpawnPosition { get; set; }
        [JsonIgnore] public List<Vector3> WanderPoints { get; set; }
        
        public (float x, float y, float z) SpawnPositionTuple { get; set; }
        public List<(float x, float y, float z)> WanderPointTuples { get; set; }

        public NpcSpawnData() //  Calls on deserialization by JSON
        {
        }
        
        public NpcSpawnData(string npcJsonFileName, Transform spawnPositionTransform, IEnumerable<Transform> wanderPointsTransforms) // calls on serialization by NpcSpawnJSONCreator
        {
            NpcJsonFileName = npcJsonFileName;
            SpawnPositionTuple = (spawnPositionTransform.position.x, spawnPositionTransform.position.y, spawnPositionTransform.position.z);
            WanderPointTuples = new List<(float x, float y, float z)>();
            foreach (var pointTransform in wanderPointsTransforms)
            {
                WanderPointTuples.Add((pointTransform.position.x, pointTransform.position.y, pointTransform.position.z));
            }
        }

        public void ConvertDataToVector3() // Calls right after deserialization to convert tuples to Vector3
        {
            SpawnPosition = new Vector3(SpawnPositionTuple.x, SpawnPositionTuple.y, SpawnPositionTuple.z);
            WanderPoints = new List<Vector3>();
            for (var i = 0; i < WanderPointTuples.Count; i++)
            {
                WanderPoints.Add(new Vector3(WanderPointTuples[i].x, WanderPointTuples[i].y, WanderPointTuples[i].z));
            }
        }
    }
}
