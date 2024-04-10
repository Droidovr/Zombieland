using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public class TakeImpactController : Controller, ITakeImpactController
    {
        public ICharacterController CharacterController { get; private set; }

        private TakerImpact _takerImpact;

        public TakeImpactController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void ApplyImpact(List<DirectImpactData> damageTakens)
        {
            _takerImpact.ApplyImpact(damageTakens);
        }

        protected override void CreateHelpersScripts()
        {
            _takerImpact = new TakerImpact(CharacterController);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}