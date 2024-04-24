using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }
        IShotProcess ShotProcess { get; }

        void Init(IWeaponController weaponController);
    }
}