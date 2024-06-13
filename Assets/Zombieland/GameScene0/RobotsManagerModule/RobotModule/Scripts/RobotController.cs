using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public class RobotController : Controller, IRobotController
    {
        public IRobotsManagerController RobotsManagerController { get; private set; }
        public RobotSpawnData RobotSpawnData { get; private set; }
        public IRobotDataController RobotDataController { get; private set; }
        public IRobotVisualBodyController RobotVisualBodyController { get; private set; }

        public RobotController(IController parentController, List<IController> requiredControllers, RobotSpawnData robotSpawnData) : base(parentController, requiredControllers)
        {
            RobotsManagerController = parentController as IRobotsManagerController;
            RobotSpawnData = robotSpawnData;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            RobotDataController = new RobotDataController(this, null);
            subsystemsControllers.Add((IController)RobotDataController);

            RobotVisualBodyController = new RobotVisualBodyController(this, new List<IController> { (IController)RobotsManagerController.RootController.EnvironmentController });
            subsystemsControllers.Add( (IController)RobotVisualBodyController);
        }
    }
}