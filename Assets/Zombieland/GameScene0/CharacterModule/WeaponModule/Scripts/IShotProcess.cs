using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotProcess
    {
        public event Action OnShotPerformed;

        IWeaponController WeaponController { get; set; }


        void Init(IWeaponController weaponController);
        void StartFire();
        void StopFire();
    }
}