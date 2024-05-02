using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;

namespace Zombieland.GameScene0.NPCManagerModule.Scripts
{
    public class NpcSpawnSearch : MonoBehaviour
    {
        public List<NpcSpawnData> GetNpcSpawnDataList()
        {
            return FindObjectsOfType<NpcSpawnData>().ToList();
        }
    }
}
