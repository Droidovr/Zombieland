using Zombieland.CharacterModule.CharacterDataModule;
using Zombieland.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }
        
        
    }
}