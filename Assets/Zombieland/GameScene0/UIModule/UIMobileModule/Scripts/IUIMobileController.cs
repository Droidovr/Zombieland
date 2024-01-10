using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIMobileController
    {
        event Action<Vector2> OnJoystickMoved;
        event Action OnFire;
    }
}
