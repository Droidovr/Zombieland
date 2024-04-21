using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public interface IAnimationController
    {
        event Action<Vector3> OnAnimationMove;
        event Action OnAnimationAttack;
        event Action<string> OnAnimationCreateWeapon;
        event Action OnAnimationDestroyWeapon;

        ICharacterController CharacterController { get; }
    }
}