using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NpcDataController : Controller, INpcDataController
    {
        public NpcData NpcData { get; private set; }
        private readonly NpcSpawnData _npcSpawnData;
        
        public NpcDataController(IController parentController, List<IController> requiredControllers, NpcSpawnData npcSpawnData) 
            : base(parentController, requiredControllers)
        {
            _npcSpawnData = npcSpawnData;
        }

        protected override void CreateHelpersScripts()
        {
            NpcData = new NpcData {SpawnData = _npcSpawnData};
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
    }
}