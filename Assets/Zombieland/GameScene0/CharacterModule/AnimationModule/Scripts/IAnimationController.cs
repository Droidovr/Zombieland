using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public interface IAnimationController
    {
        ICharacterController CharacterController { get; }
        Animator Animator { get; }
    }
}