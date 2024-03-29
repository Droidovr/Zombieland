using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NPCSpawnController : Controller, INPCSpawnController
    {
        private readonly INPCController NPCController;

        public NPCSpawnController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NPCController = (INPCController)parentController;
            CreateHelpersScripts();
            TestCreateSubsystems();
        }

        protected override void CreateHelpersScripts()
        {
            SetAndActivateNPC();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystems()
        {
            
        }

        private void SetAndActivateNPC()
        {
            var activeNPC = NPCController.VisualBodyController.ActiveNPC;
            activeNPC.transform.position = NPCController.DataController.NPCData.SpawnPosition;
            activeNPC.SetActive(true);
        }
    }
}
