using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class NpcVisionSensorController : Controller, INpcVisionSensorController
    {
        private readonly INpcAwarenessController _npcAwarenessController;
        private VisionSensor _visionSensor;

        public NpcVisionSensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcAwarenessController = (INpcAwarenessController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _visionSensor = _npcAwarenessController.NpcController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init( _npcAwarenessController.NpcController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _visionSensor =  _npcAwarenessController.NpcController.VisualBodyController.ActiveNPC.GetComponent<VisionSensor>();
            _visionSensor.Init( _npcAwarenessController.NpcController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        private void OnCharacterInsideZone(bool isInsideZone)
        {
            _npcAwarenessController.CanSeeTarget(isInsideZone);
        }
    }
}
