using System;
using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ITestUIController
    {
        event Action<Vector2> OnJoustickMoved;
    }
}
