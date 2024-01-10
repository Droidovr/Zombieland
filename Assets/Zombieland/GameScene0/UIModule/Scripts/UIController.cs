using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.UIModule
{
    public class UIController : Controller, IUIController
    {
        public IUIMobileController UIMobileController { get; private set; }
        public IUIPCController UIPCController { get; private set; }

        /// Mobile controllerSelection = 0
        /// PC controllerSelection = 1
        private int _controllerSelection = 1;

        private readonly IRootController _rootController;

        public UIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            _rootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller does not have helpers scripts.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            if (_controllerSelection == 0)
            {
                UIMobileController = new UIMobileController(this, null);
                subsystemsControllers.Add((IController)UIMobileController);
            }

            if (_controllerSelection == 1)
            {
                UIPCController = new UIPCController(this, null);
                subsystemsControllers.Add((IController)UIPCController);
            }
        }
    }
}
