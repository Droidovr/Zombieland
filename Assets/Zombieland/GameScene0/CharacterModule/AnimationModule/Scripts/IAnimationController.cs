using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public interface IAnimationController
    {
        event Action<Vector3> OnAnimatorMove;
        event Action OnFinishPreparationAttack;
        event Action<string> OnFinishWeaponAnimation;
        ICharacterController CharacterController { get; }
    }
}