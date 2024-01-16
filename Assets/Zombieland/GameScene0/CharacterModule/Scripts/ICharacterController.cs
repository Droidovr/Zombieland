using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        IRootController RootController { get; }
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
        ICharacterMovingController CharacterMovingController { get; }
    }
}