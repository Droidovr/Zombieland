using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIMain
    {
        event Action<Vector2> OnMoved;
        event Action OnFire;
    }
}