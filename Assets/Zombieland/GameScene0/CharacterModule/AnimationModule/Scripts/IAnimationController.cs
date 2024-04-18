using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public interface IAnimationController
    {
        event Action<Vector3> OnAnimatorMove;
        event Action OnFinishPreparationAttack;
        event Action<string> OnFinishWeaponAnimation;
        event Action OnCreateWeaponPrefab;
        event Action OnDestroyWeaponPrefab;

        ICharacterController CharacterController { get; }
    }
}