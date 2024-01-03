using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        ICharacterController CharacterController { get; }
        void ChangeWeapon();
        void Fire();
    }
}