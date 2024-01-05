using Zombieland.CharacterModule.CharacterDataModule;
using Zombieland.CharacterModule.WeaponModule;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        IRootController RootController { get; }
        ICharacterDataController CharacterDataController { get; }
        ICharacterMovingController CharacterMovingController { get; }
        IWeaponController WeaponController { get; }
    }
}