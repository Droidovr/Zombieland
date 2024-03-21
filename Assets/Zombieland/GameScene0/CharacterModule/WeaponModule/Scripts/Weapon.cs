using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class Weapon : IWeapon
    {
        public event Action OnShot;

        public WeaponData WeaponData { get; set; }
        public IShotProcess ShotProcess { get; set; }
    }
}