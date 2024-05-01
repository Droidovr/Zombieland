using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NpcSpawnController : Controller, INpcSpawnController
    {
        private readonly INpcController _NPCController;

        public NpcSpawnController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INpcController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            SetAndActivateNPC();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void SetAndActivateNPC()
        {
            var activeNpc = _NPCController.NpcVisualBodyController.ActiveNPC;
            activeNpc.transform.position = _NPCController.NpcDataController.NpcData.spawnPosition;
            activeNpc.SetActive(true);
        }
    }
}
