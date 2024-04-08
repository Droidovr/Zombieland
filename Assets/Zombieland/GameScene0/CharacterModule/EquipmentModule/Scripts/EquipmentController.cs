using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public class EquipmentController : Controller, IEquipmentController
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action<string> OnEquipmentChanged;
        public event Action<string> OnAmmoChanged;
        public event Action OnAmmoDepleted;

        public ICharacterController CharacterController { get; private set; }
        public Dictionary<int, Weapon> WeaponsSlots { get; private set; }
        public int CurrentAmmoCount { get; private set; }
        public string CurrentOutfitEquipped { get; private set; }


        public EquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public override void Enable()
        {
            WeaponsSlots = new Dictionary<int, Weapon>() { { 1, null }, { 2, null }, { 3, null }, { 4, null } };

            CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 += Number4Handler;

            CharacterController.InventoryController.OnMainSlotEquipped += MainSlotEquippedHandler;
            //CharacterController.InventoryController.OnCurrentImpactEquipped += CurrentImpactEquippedHandler;
            CharacterController.InventoryController.OnCurrentOutfitEquipped += CurrentOutfitEquippedHandler;

            base.Enable();
        }

        public override void Disable()
        {
            CharacterController.RootController.UIController.OnNumber1 -= Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 -= Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 -= Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 -= Number4Handler;

            CharacterController.InventoryController.OnMainSlotEquipped -= MainSlotEquippedHandler;
            //CharacterController.InventoryController.OnCurrentImpactEquipped -= CurrentImpactEquippedHandler;
            CharacterController.InventoryController.OnCurrentOutfitEquipped -= CurrentOutfitEquippedHandler;

            base.Disable();
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

        #region PRIVATE
        private void MainSlotEquippedHandler(string name, int slotNumber)
        {
            WeaponsSlots[slotNumber] = CharacterController.RootController.GameDataController.GetData<Weapon>(name); //*Place new Weapon Object here*;
        }

        private void CurrentOutfitEquippedHandler(string name)
        {
            OnEquipmentChanged?.Invoke(name);
        }

        private void Number1Handler()
        {
            Debug.Log("Weapon slot 1 is empty!");
            if (WeaponsSlots[1] != null)
            {
                Debug.Log($"Weapon in slot 1 : {WeaponsSlots[1]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[1]);
            }
        }

        private void Number2Handler()
        {
            Debug.Log("Weapon slot 2 is empty!");
            if (WeaponsSlots[2] != null)
            {
                Debug.Log($"Weapon in slot 2 : {WeaponsSlots[2]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[2]);
            }
        }

        private void Number3Handler()
        {
            Debug.Log("Weapon slot 3 is empty!");
            if (WeaponsSlots[3] != null)
            {
                Debug.Log($"Weapon in slot 3 : {WeaponsSlots[3]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[3]);
            }
        }

        private void Number4Handler()
        {
            Debug.Log("Weapon slot 4 is empty!");
            if (WeaponsSlots[4] != null)
            {
                Debug.Log($"Weapon in slot 4 : {WeaponsSlots[4]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[4]);
            }
        }
        #endregion PRIVATE
    }
}