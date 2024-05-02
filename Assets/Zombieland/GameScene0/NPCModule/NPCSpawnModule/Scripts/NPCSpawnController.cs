using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NpcSpawnController : Controller, INpcSpawnController
    {
        private readonly INpcController _npcController;

        public NpcSpawnController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            ActivateNpc();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void ActivateNpc()
        {
            _npcController.NpcVisualBodyController.NpcOnScene.transform.position = _npcController.NpcDataController.NpcData.SpawnData.SpawnPosition;
            _npcController.NpcVisualBodyController.NpcOnScene.SetActive(true);
        }
    }
}