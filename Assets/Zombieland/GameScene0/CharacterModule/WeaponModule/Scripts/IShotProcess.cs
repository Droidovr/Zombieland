using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotProcess
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;

        ICharacterController Owner { get; set; }


        void StartFire();
        void StopFire();
    }
}