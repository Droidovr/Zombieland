using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class NPCVisionSensorController : Controller, INPCVisionSensorController
    {
        private readonly INPCController _NPCController;
        private VisionSensor _visionSensor;

        public NPCVisionSensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INPCController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _visionSensor = _NPCController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init(_NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _visionSensor = _NPCController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init(_NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        private void OnCharacterInsideZone(bool isInsideZone)
        {
            
        }
    }
}
