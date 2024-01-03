
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public ICharacterController CharacterController { get; }


        public WeaponController(IController parentController)
        {
            CharacterController = (ICharacterController) parentController;
        }
        
        public void ChangeWeapon()
        {
            
        }

        public void Fire()
        {
            
        }
        
        
        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            
        }
    }
}