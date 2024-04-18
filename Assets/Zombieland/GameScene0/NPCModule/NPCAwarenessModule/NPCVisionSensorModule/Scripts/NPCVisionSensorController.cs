using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class NPCVisionSensorController : Controller, INPCVisionSensorController
    {
        private readonly INPCAwarenessController _INPCAwarenessController;
        private VisionSensor _visionSensor;

        public NPCVisionSensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _INPCAwarenessController = (INPCAwarenessController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _visionSensor = _INPCAwarenessController.NPCController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init( _INPCAwarenessController.NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _visionSensor =  _INPCAwarenessController.NPCController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init( _INPCAwarenessController.NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        private void OnCharacterInsideZone(bool isInsideZone)
        {
            _INPCAwarenessController.CanSeeTarget(isInsideZone);
        }
    }
}
