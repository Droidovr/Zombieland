using System.Collections.Generic;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public class RobotController : Controller, IRobotController
    {
        public IRobotsManagerController RobotsManagerController { get; private set; }
        public RobotSpawnData RobotSpawnData { get; private set; }

        public RobotController(IController parentController, List<IController> requiredControllers, RobotSpawnData robotSpawnData) : base(parentController, requiredControllers)
        {
            RobotsManagerController = parentController as IRobotsManagerController;
            RobotSpawnData = robotSpawnData;
        }

        protected override void CreateHelpersScripts()
        {

        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {

        }
    }
}