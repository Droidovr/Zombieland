using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIController
    {
        event Action<Vector2> OnJoystickMoved;
    }
}
