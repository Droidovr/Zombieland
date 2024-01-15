using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }
        ICharacterMovingController CharacterMovingController { get; }
    }
}