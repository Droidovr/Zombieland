using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NpcDataController : Controller, INpcDataController
    {
        public NpcData NpcData { get; set; }
        private readonly INpcController _npcController;
        
        public NpcDataController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            NpcData = new NpcData();
            NpcData.spawnPosition = _npcController.NpcSpawnData.transform.position;
            foreach (var wanderTransform in _npcController.NpcSpawnData.wanderPositionTransforms)
            {
                NpcData.wanderPositions.Add(wanderTransform.position);
            }
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
    }
}