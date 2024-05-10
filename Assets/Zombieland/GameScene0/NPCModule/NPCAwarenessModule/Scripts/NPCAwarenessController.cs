using System;
using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public class NPCAwarenessController : Controller, INPCAwarenessController
    {
        public event Action<IController> OnHearingSound;

        public INPCController NPCController { get; private set; }
        public INPCHearingController NPCHearingController { get; private set; }

        public NPCAwarenessController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            NPCHearingController = new NPCHearingController(this, new List<IController>
                {
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController,
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.StealthController,
                    (IController)NPCController.NPCMovingController
                });
            subsystemsControllers.Add((IController)NPCHearingController);
        }
    }
}