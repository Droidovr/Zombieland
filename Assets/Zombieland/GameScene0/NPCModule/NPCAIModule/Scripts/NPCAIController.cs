using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCAIController : Controller, INPCAIController
    {
        public event Action SlotNumber1;
        public event Action SlotNumber2;
        public event Action SlotNumber3;
        public event Action SlotNumber4;
        public event Action<bool> OnFire;

        public INPCController NPCController { get; private set; }
        private NPCPatrolling _nPCPatrolling;

        public NPCAIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        protected override void CreateHelpersScripts()
        {
            _nPCPatrolling = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCPatrolling>();
            _nPCPatrolling.Init(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}