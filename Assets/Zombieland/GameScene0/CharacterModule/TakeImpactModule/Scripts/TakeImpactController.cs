using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.CharacterModule.TakeImpactModule
{
    public class TakeImpactController : Controller, ITakeImpactController
    {
        public TakeImpactController(IController parentController, List<IController> requiredControllers)
            : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        public void ApplyImpact(List<DirectImpactData> directImpactDataList)
        {
            // This method doesn't have any realization at the moment.
        }
    }
}