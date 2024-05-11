using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public class NPCVisionController : Controller, INPCVisionController
    {
        public event Action<IController, bool> OnVisualDetect;

        public INPCAwarenessController NPCAwarenessController { get; private set; }

        private VisionSensor _visualSensor;

        public NPCVisionController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCAwarenessController = parentController as INPCAwarenessController;
        }

        protected override void CreateHelpersScripts()
        {
            _visualSensor = NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.AddComponent<VisionSensor>();
            _visualSensor.Init(this);
            _visualSensor.OnVisualDetect += VisualDetectHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }


        private void VisualDetectHandler(IController controller, bool isVisible)
        {
            OnVisualDetect?.Invoke(controller, isVisible);
        }
    }
}