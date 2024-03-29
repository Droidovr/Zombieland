using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.SensorModule.EnvironmentSensorModule
{
    public class EnvironmentSensorController : Controller, IEnvironmentSensorController
    {
        public ISensorController SensorController { get; private set; }

        public EnvironmentSensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            SensorController = parentController as ISensorController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}