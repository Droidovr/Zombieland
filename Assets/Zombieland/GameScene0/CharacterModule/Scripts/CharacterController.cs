using System.Collections.Generic;
using Zombieland.CharacterModule.CharacterDataModule;
using Zombieland.CharacterModule.WeaponModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public class CharacterController : Controller, ICharacterController
    {
        public IRootController RootController { get; }
        public ICharacterDataController CharacterDataController { get; private set; }
        public IWeaponController WeaponController { get; private set; }


        public CharacterController(IController parentController)
        {
            RootController = (IRootController) parentController;
        }
        
        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterDataController = new CharacterDataController(this);
            subsystemsControllers.Add((IController)CharacterDataController);

            WeaponController = new WeaponController(this);
            subsystemsControllers.Add((IController)WeaponController);
        }
    }
}
