using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NPCDataController : Controller, INPCDataController
    {
        public NPCData NPCData { get; set; }

        public NPCDataController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            CreateHelpersScripts();
            TestCreateSubsystems();
        }

        protected override void CreateHelpersScripts()
        {
            NPCData = new NPCData();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystems()
        {
            
        }
    }
}
