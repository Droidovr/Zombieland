using System.Collections.Generic;

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
            _impactDetectionSensor = _characterController.VisualBodyController.SensorCollider.gameObject.AddComponent<ImpactDetectionSensor>();
            _impactDetectionSensor.Init((IImpactable)_characterController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
