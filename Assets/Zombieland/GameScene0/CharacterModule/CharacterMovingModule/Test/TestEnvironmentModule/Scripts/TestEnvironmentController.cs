using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class EnvironmentController : Controller, ITestEnvironmentController
    {
        public readonly IRootController RootController;

        private TestInitializerEnvironment _initializerEnvironment;

        public EnvironmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
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
            _initializerEnvironment = new TestInitializerEnvironment();
            _initializerEnvironment.Init();
        }
    }
}