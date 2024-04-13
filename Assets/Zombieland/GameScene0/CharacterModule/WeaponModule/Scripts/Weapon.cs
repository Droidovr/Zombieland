using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class Weapon : IWeapon
    {
        public WeaponData WeaponData { get; set; }
        public IShotProcess ShotProcess { get; set; }

        public void Init(IWeaponController weaponController)
        {
            ShotProcess.Init(weaponController);
        }
    }
}