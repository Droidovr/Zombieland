using System.Collections.Generic;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GlobalSoundModule
{
    public class GlobalSoundController : Controller, IGlobalSoundController
    {
        public IRootController RootController { get; private set; }

        private GlobalAudiosource _globalAudiosource;

        public GlobalSoundController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
        }

        protected override void CreateHelpersScripts()
        {
            _globalAudiosource = new GlobalAudiosource();
            _globalAudiosource.CreateGlobalAudioSource();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}