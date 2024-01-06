
using System.Collections.Generic;

namespace Zombieland.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
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

        public void ChangeWeapon()
        {
           // throw new System.NotImplementedException();
        }

        public void Fire()
        {
           // throw new System.NotImplementedException();
        }
    }
}