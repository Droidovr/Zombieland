using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        event Action<Vector2> OnMoved;
    }
}