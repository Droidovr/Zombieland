using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
    }
}