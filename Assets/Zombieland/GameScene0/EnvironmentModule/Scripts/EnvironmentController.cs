using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.EnvironmentModule
{
    public class EnvironmentController : Controller, IEnvironmentController
    {
        private IRootController _rootController;

        private InitializerEnvironment _initializerEnvironment;

        public EnvironmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            CreateEnvironmentObjects();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void CreateEnvironmentObjects()
        {
            _initializerEnvironment = new InitializerEnvironment();
            _initializerEnvironment.Init();
        }
    }
}