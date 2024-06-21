using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public class RobotDeadBodySensorController : Controller, IRobotDeadBodySensorController
    {
        public IRobotAwarenesController RobotAwarenesController { get; private set; }


        public RobotDeadBodySensorController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotAwarenesController = parentController as IRobotAwarenesController;
        }

        protected override void CreateHelpersScripts()
        {
            switch (RobotAwarenesController.RobotController.RobotDataController.RobotData.RobotType)
            {
                case RobotType.Graber:
                    RobotAwarenesController.RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotDeadBodySensor>();
                    break;

                default:
                    break;
            }
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}