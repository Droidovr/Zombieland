using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }
        IShotProcess ShotProcess { get; set; }

        void Init(IWeaponController weaponController);
    }
}