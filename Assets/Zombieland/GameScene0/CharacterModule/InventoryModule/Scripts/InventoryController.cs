using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.CharacterModule.InventoryModule
{
    public class InventoryController : Controller, IInventoryController
    {
        public event Action<string, int> OnMainSlotEquipped;
        public event Action<string, int> OnCurrentImpactEquipped;
        public event Action<string> OnCurrentOutfitEquipped;

        public Dictionary<string, int> ItemsInInventory { get; private set; }

        public ICharacterController CharacterController { get; private set; }

        public InventoryController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void PickUpItem(string itemName, int count)
        {

        }

        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED
    }
}

