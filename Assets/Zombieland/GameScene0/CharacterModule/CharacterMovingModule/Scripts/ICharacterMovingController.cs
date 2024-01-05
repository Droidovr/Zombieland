using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        ICharacterController CharacterController { get; }

        event Action<Vector2> OnJoustickMoved;
    }
}