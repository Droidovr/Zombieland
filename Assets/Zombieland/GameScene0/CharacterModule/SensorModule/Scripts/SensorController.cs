using System.Collections.Generic;
using Zombieland.GameScene0.ProjectileModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class SensorController : Controller, ISensorController
    {
        private readonly ICharacterController _characterController;
        private DamageDetectionSensor _damageDetectionSensor;
        
        public SensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _characterController = (ICharacterController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            //var detectionCollider = _characterController.VisualBodyController.GetCollider
            //add _damageDetectionSensor
            _damageDetectionSensor.Init(TakeDamage);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void TakeDamage(IProjectileController projectileController)
        {
            _characterController.TakeDamage(projectileController);
        }
    }
}
