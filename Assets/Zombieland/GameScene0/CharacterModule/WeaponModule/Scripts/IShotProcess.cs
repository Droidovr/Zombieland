using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotProcess
    {
        event Action OnShotPerformed;

        void Init(IWeaponController weaponController);
        void StartFire();
        void StopFire();
    }
}