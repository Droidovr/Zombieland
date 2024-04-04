using System;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        event Action OnShot;

        WeaponData WeaponData { get; set; }
        //IShotProcess ShotProcess { get; set; }
    }
}