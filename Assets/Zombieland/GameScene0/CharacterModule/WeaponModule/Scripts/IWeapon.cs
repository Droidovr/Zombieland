using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        WeaponData WeaponData { get; set; }

        IShotProcess ShotProcess { get; set; }
    }
}