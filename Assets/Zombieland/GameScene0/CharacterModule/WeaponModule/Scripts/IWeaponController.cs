using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        event Action OnShotPerformed;
        event Action OnShotFailed;

        ICharacterController CharacterController { get; }
        IWeapon Weapon { get; }
    }
}