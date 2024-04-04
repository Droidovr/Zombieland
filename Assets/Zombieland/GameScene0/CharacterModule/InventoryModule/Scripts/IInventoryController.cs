using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.InventoryModule
{
    public interface IInventoryController
    {
        event Action<string, int> OnMainSlotEquipped;
        event Action<string, int> OnCurrentImpactEquipped;
        event Action<string> OnCurrentOutfitEquipped;

        Dictionary<string, int> ItemsInInventory { get; }

        ICharacterController CharacterController { get; }

        void PickUpItem(string itemName, int count);
    }
}
