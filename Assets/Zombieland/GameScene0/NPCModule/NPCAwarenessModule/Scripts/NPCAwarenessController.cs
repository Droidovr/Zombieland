using System;
using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public class NPCAwarenessController : Controller, INPCAwarenessController
    {
        public event Action<IController> OnHearingSound;

        public INPCController NPCController { get; private set; }
        public INPCHearingController NPCHearingController { get; private set; }
        public INPCVisionController NPCVisionController { get; private set; }

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
                    (IController)NPCController.NPCVisualBodyController,
                    (IController)NPCController.NPCMovingController
                });
            subsystemsControllers.Add((IController)NPCHearingController);

            NPCVisionController = new NPCVisionController(this, new List<IController>
                {
                    (IController)NPCController.NPCManagerController.RootController.CharacterController.StealthController,
                    (IController)NPCController.NPCVisualBodyController
                });
            subsystemsControllers.Add((IController)NPCVisionController);
        }
    }
}