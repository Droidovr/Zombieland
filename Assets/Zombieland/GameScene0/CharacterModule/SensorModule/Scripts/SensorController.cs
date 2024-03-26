using System.Collections.Generic;
using Unity.VisualScripting;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class SensorController : Controller, ISensorController
    {
        private readonly ICharacterController _characterController;
        private ImpactDetectionSensor _impactDetectionSensor;
        public SensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _characterController = (ICharacterController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            //var impactSensor = _characterController.VisualBodyController.SensorCollider.AddComponent<ImpactSensor>();
            //impactSensor.Owner = _characterController;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
