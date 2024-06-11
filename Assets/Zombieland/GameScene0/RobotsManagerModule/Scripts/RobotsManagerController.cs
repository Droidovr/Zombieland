using System.Collections.Generic;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule;
using Zombieland.GameScene0.RootModule;


namespace Zombieland.GameScene0.RobotsManagerModule
{
    public class RobotsManagerController : Controller, IRobotsManagerController
    {
        public IRootController RootController { get; private set; }
        public List<IRobotController> ActiveRobotControllers { get; private set; }

        public RobotsManagerController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
            ActiveRobotControllers = new List<IRobotController>();
        }


        public void AddRobotToActive(IRobotController robotController)
        {
            ActiveRobotControllers.Add(robotController);
        }

        public void RemoveRobotFromActive(IRobotController robotController)
        {
            ActiveRobotControllers.Remove(robotController);
        }

        protected override void CreateHelpersScripts()
        {

        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            var robotsSpawnDataList = RootController.GameDataController.GetData<List<RobotSpawnData>>("RobotsSpawnData");
            foreach (var robotSpawnData in robotsSpawnDataList)
            {
                IRobotController robotController = new RobotController(this, null, robotSpawnData);
                subsystemsControllers.Add((IController)robotController);
                AddRobotToActive(robotController);
            }
        }
    }
}