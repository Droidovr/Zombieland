using System;
using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        event Action<Vector2> OnJoustickMoved;
    }
}