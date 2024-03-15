using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.ImpactModule;
using System.Linq;
using Assets.Zombieland.GameScene0.CharacterModule.TakeImpactModule.Scripts;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public class TakeImpactController : Controller, ITakeImpactController
    {

        private ICharacterController _characterController;
        private TakeImpactHandler _takeImpactHandler;

        public TakeImpactController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _characterController = parentController as ICharacterController;
          
        }

        public void processImpact(DirectImpactData directImpactData)
        {
            _takeImpactHandler.handleImpact(directImpactData);

        }

        protected override void CreateHelpersScripts()
        {
            _takeImpactHandler = new TakeImpactHandler();
            _takeImpactHandler.Init(_characterController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

    }
}