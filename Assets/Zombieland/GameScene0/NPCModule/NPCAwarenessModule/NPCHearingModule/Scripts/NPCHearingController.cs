using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public class NPCHearingController : Controller, INPCHearingController
    {
        public event Action<IController, bool> OnHearingDetect;

        public INPCAwarenessController NPCAwarenessController { get; private set; }

        private HearingSensor _hearingSensor;

        public NPCHearingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCAwarenessController = parentController as INPCAwarenessController;
        }

        public override void Disable()
        {
            _hearingSensor.Destroy();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _hearingSensor = new HearingSensor(this);
            _hearingSensor.OnHearingDetect += HearingDetectHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn�t have any subsystems at the moment.
        }


        private void HearingDetectHandler(IController controller, bool isHearing)
        {
            OnHearingDetect?.Invoke(controller, isHearing);
        }
    }
}