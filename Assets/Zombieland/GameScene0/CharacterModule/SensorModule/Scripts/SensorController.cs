using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class SensorController : Controller, ISensorController
    {
        private readonly ICharacterController _characterController;
        
        public SensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _characterController = (ICharacterController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            //add _damageDetectionSensor to character collider
            //_impactDetectionSensor.Init((IDamageable)_characterController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
