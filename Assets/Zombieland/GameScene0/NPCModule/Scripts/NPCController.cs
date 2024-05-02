using System.Collections.Generic;
using UnityEngine;
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
        public INpcManagerController NpcManagerController { get; private set; }
        public INpcDataController NpcDataController { get; private set; }
        public INpcVisualBodyController NpcVisualBodyController { get; private set; }
        public INpcSpawnController NpcSpawnController { get; private set; }
        public INpcMovingController NpcMovingController { get; private set; }
        public INpcTakeImpactController NpcTakeImpactController { get; private set; }
        public INpcAwarenessController NpcAwarenessController { get; private set; }
        public INpcAIController NpcAIController { get; set; }
        private readonly NpcSpawnData _npcSpawnData;

        public NpcController(IController parentController, List<IController> requiredControllers, NpcSpawnData npcSpawnData) 
            : base(parentController, requiredControllers)
        {
            NpcManagerController = (INpcManagerController) parentController;
            _npcSpawnData = npcSpawnData;
        }

        protected override void CreateHelpersScripts()
        {
            //This method has no implementation
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            NpcDataController = new NpcDataController(this, null, _npcSpawnData);
            subsystemsControllers.Add((IController)NpcDataController);
            
            NpcVisualBodyController = new NpcVisualBodyController(this, new List<IController>{(IController)NpcDataController});
            subsystemsControllers.Add((IController)NpcVisualBodyController);
            
            NpcSpawnController = new NpcSpawnController(this, new List<IController>{(IController)NpcDataController, (IController)NpcVisualBodyController});
            subsystemsControllers.Add((IController)NpcSpawnController);
            
            NpcMovingController = new NpcMovingController(this, new List<IController>{(IController)NpcDataController, (IController)NpcVisualBodyController});
            subsystemsControllers.Add((IController)NpcMovingController);

            NpcTakeImpactController = new NpcTakeImpactController(this,new List<IController>{(IController)NpcDataController});
            subsystemsControllers.Add((IController)NpcTakeImpactController);
            
            NpcAwarenessController = new NpcAwarenessController(this, new List<IController>{(IController)NpcDataController, (IController)NpcVisualBodyController, (IController)NpcSpawnController});
            subsystemsControllers.Add((IController)NpcAwarenessController);
            
            NpcAIController = new NpcAIController(this, new List<IController>{(IController)NpcDataController, (IController)NpcVisualBodyController, (IController)NpcSpawnController});
            subsystemsControllers.Add((IController)NpcAIController);
        }
    }
}
