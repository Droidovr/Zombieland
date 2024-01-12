using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIPCController
    {
        event Action<Vector2> OnKeyboardMoved;
        event Action OnFire;
    }
}