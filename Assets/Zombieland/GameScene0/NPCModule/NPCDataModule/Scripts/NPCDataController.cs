using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCDataModule
{
    public class NpcDataController : Controller, INpcDataController
    {
        public NpcData NPCData { get; set; }

        public NpcDataController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            NPCData = new NpcData();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void TestCreateSubsystem()
        {
            NPCData = new NpcData();
        }
    }
}
