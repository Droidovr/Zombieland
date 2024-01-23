using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public class EquipmentController : Controller, IEquipmentController
    {
        public EquipmentController(IController parentController, List<IController> requiredControllers) 
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
            // This method doesn't have any realization at the moment.
        }
    }
}