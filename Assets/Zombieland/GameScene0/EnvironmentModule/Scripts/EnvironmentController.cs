using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class EnvironmentController : Controller, IEnvironmentController
    {
        public string CurrentLevelName { get; private set; }

        private IRootController _rootController;

        private InitializerEnvironment _initializerEnvironment;

        public EnvironmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
            _initializerEnvironment = new InitializerEnvironment();
        }

        protected override void CreateHelpersScripts()
        {
            EnvironmentData environmentData = _rootController.GameDataController.GetData<EnvironmentData>("EnvironmentData");
            CurrentLevelName = environmentData.CurrentLevelName;
            _initializerEnvironment.Init(environmentData);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}