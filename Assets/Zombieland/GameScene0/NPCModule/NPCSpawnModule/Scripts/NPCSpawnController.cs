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
            TestCreateSubsystem();
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
            var activeNPC = _NPCController.VisualBodyController.ActiveNPC;
            activeNPC.transform.position = _NPCController.DataController.NPCData.spawnPosition;
            activeNPC.SetActive(true);
        }
        
        private void TestCreateSubsystem()
        {
            SetAndActivateNPC();
        }
    }
}
