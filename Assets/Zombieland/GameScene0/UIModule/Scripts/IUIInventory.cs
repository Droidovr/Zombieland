using System;

namespace Zombieland.GameScene0.UIModule
{
    public interface IUIInventory
    {
        event Action<string> OnInventoryButtonClick;
    }
}
