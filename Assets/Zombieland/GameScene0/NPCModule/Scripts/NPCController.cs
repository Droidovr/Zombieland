using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.AIModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCTakeImpactModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public class NpcController : Controller, INpcController
    {
        public INpcManagerController NPCManagerController { get; set; }
        public INpcDataController DataController { get; set; }
        public INpcVisualBodyController VisualBodyController { get; set; }
        public INpcSpawnController SpawnController { get; set; }
        public INpcMovingController MovingController { get; set; }
        public INpcTakeImpactController TakeImpactController { get; set; }
        public INpcAwarenessController AwarenessController { get; set; }
        public INpcAIController AIController { get; set; }

        public NpcController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NPCManagerController = (INpcManagerController) parentController;
            TestCreateSubsystems();
        }

        protected override void CreateHelpersScripts()
        {
            //This method has no implementation
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            DataController = new NpcDataController(this, null);
            subsystemsControllers.Add((IController)DataController);
            
            VisualBodyController = new NpcVisualBodyController(this, new List<IController>{(IController)DataController});
            subsystemsControllers.Add((IController)VisualBodyController);
            
            SpawnController = new NpcSpawnController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)SpawnController);
            
            MovingController = new NpcMovingController(this, new List<IController>{(IController)VisualBodyController, (IController)DataController});
            subsystemsControllers.Add((IController)MovingController);

            TakeImpactController = new NpcTakeImpactController(this,new List<IController>{(IController)DataController});
            subsystemsControllers.Add((IController)TakeImpactController);
            
            AwarenessController = new NpcAwarenessController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)AwarenessController);
            
            AIController = new NpcAIController(this, new List<IController>{(IController)VisualBodyController});
            subsystemsControllers.Add((IController)AIController);
        }

        private void TestCreateSubsystems()
        {
            DataController = new NpcDataController(this, new List<IController>());
            VisualBodyController = new NpcVisualBodyController(this, new List<IController>{(IController)DataController});
            SpawnController = new NpcSpawnController(this, new List<IController>{(IController)VisualBodyController});
            MovingController = new NpcMovingController(this, new List<IController>{(IController)VisualBodyController, (IController)DataController});
            TakeImpactController = new NpcTakeImpactController(this,new List<IController>{(IController)DataController});
            AwarenessController = new NpcAwarenessController(this, new List<IController>{(IController)VisualBodyController});
            AIController = new NpcAIController(this, new List<IController>{(IController)VisualBodyController});
        }
    }
}
