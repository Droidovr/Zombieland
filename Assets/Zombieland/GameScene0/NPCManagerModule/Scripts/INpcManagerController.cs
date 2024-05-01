using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public interface INpcManagerController
    {
        public IRootController RootController { get; }
        public Transform CharacterTransform { get; }

        public List<INpcController> ActiveNpcControllers { get; }
        public void AddNpcToActive(INpcController npcController);
        public void RemoveNpcFromActive(INpcController npcController);
    }
}
