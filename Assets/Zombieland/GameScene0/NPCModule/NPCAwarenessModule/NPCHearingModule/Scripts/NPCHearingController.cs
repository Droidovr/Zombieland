using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public class NPCHearingController : Controller, INPCHearingController
    {
        public INPCAwarenessController NPCAwarenessController { get; private set; }

        private HearingSensor _hearingSensor;

        public NPCHearingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCAwarenessController = parentController as INPCAwarenessController;
        }

        protected override void CreateHelpersScripts()
        {
            _hearingSensor = new HearingSensor(this);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}