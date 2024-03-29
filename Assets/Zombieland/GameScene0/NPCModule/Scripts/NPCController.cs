using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public class NPCController : Controller, INPCController
    {
        public INPCDataController DataController { get; set; }
        public INPCVisualBodyController VisualBodyController { get; set; }
        public INPCSpawnController SpawnController { get; set; }
        public INPCMovingController MovingController { get; set; }
        
        public NPCController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            CreateHelpersScripts();
            TestCreateSubsystems();
        }

        protected override void CreateHelpersScripts()
        {
            //This method has no implementation
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            DataController = new NPCDataController(this, new List<IController>());
            subsystemsControllers.Add((IController)DataController);
            
            VisualBodyController = new NPCVisualBodyController(this, new List<IController>{(IController)DataController});
            subsystemsControllers.Add((IController)VisualBodyController);
            
            SpawnController = new NPCSpawnController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)SpawnController);
        }

        private void TestCreateSubsystems()
        {
            DataController = new NPCDataController(this, new List<IController>());
            VisualBodyController = new NPCVisualBodyController(this, new List<IController>{(IController)DataController});
            SpawnController = new NPCSpawnController(this, new List<IController>{(IController)VisualBodyController});
        }
    }
}
