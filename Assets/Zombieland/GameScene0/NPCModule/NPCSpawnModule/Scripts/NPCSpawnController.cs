using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NPCSpawnController : Controller, INPCSpawnController
    {
        private readonly INPCController _NPCController;

        public NPCSpawnController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INPCController)parentController;
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
            activeNPC.transform.position = _NPCController.DataController.NPCData.SpawnPosition;
            activeNPC.SetActive(true);
        }
        
        private void TestCreateSubsystem()
        {
            SetAndActivateNPC();
        }
    }
}
