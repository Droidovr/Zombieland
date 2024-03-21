using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotProcess
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        ICharacterController Owner { get; set; }
        float TimeBetweenShots { get; set; }
        float TimeBetweenRecharges { get; set; }


        void StartFire();
        void StopFire();
    }
}