using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;

namespace Zombieland.GameScene0.NPCManagerModule.Scripts
{
    public class NpcSearch : MonoBehaviour
    {
        public List<NpcSpawnData> GetNpcSpawnDataList()
        {
            var npcSpawnDataList = FindObjectOfType<NpcSpawnData>();
            return new List<NpcSpawnData>{npcSpawnDataList};
        }
    }
}
