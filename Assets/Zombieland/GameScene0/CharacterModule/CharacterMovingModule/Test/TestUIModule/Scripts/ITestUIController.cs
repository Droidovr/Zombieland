using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ITestUIController
    {
        event Action<Vector2> OnJoustickMoved;
    }
}
