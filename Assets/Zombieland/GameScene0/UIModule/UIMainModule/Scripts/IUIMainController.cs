using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIMainController
    {
        event Action<Vector2> OnMoved;
        event Action<string> OnButtonClick;
        event Action OnButtonFireDown;
        event Action OnButtonFireUp;
    }
}