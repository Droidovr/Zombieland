using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NpcDataController : Controller, INpcDataController
    {
        public NpcData NpcData { get; private set; }
        private readonly INpcController _npcController;
        private readonly NpcSpawnData _npcSpawnData;

        public NpcDataController(IController parentController, List<IController> requiredControllers, NpcSpawnData npcSpawnData) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController) parentController;
            _npcSpawnData = npcSpawnData;
        }

        protected override void CreateHelpersScripts()
        {
            NpcData = _npcController.NpcManagerController.RootController.GameDataController.GetData<NpcData>(
                _npcSpawnData.NpcJsonFileName);
            NpcData.SpawnData = _npcSpawnData;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
    }
}