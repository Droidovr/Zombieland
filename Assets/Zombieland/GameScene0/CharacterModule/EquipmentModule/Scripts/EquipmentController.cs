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
        public event Action<string> OnImpactChanged;
        public event Action OnImpactDepleted;

        public ICharacterController CharacterController { get; private set; }
        public Dictionary<int, WeaponSlot> WeaponsSlots { get; private set; }
        public Dictionary<string, int> CurrentImpactsEquipped { get; private set; }
        public int CurrentImpactCount { get;  set; } // тут в сеттері перевіряти чи можемо ми зняти патрони and so on
        public string CurrentOutfitEquipped { get; private set; }

        private Weapon _currentWeaponEquipped;
        private Tuple<string, int> _impactInstanceEquipped;


        public EquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void ReloadCurrentWeapon()
        {
            // add impacts equal to maxWeaponImpact 
        }

        public override void Enable()
        {
            WeaponsSlots = new Dictionary<int, WeaponSlot>() { { 1, WeaponSlot.empty }, { 2, WeaponSlot.empty }, { 3, WeaponSlot.empty }, { 4, WeaponSlot.empty } };

            CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 += Number4Handler;

            CharacterController.InventoryController.OnMainSlotEquipped += MainSlotEquippedHandler;
            CharacterController.InventoryController.OnCurrentImpactEquipped += CurrentImpactEquippedHandler;
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
            CharacterController.InventoryController.OnCurrentImpactEquipped -= CurrentImpactEquippedHandler;
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
            WeaponsSlots[slotNumber].SetEquippedWeapon(CharacterController.RootController.GameDataController.GetData<Weapon>(name));
        }

        private void CurrentImpactEquippedHandler(string impactID, int amount)
        {
            if (_currentWeaponEquipped.WeaponData.AvailableImpactIDs.Contains(impactID))
            {
                CurrentImpactsEquipped.Add(impactID, amount);
            }
        }

        //probably CurrentImpactSelectedHandler(string impactID) is needed, subscribed to some UIMain event and telling us which Impacts do we want to use.

        private void CurrentOutfitEquippedHandler(string name)
        {
            OnEquipmentChanged?.Invoke(name);
        }

        private void Number1Handler()
        {
            Debug.Log("Weapon slot 1 is empty!");
            if (WeaponsSlots[1] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 1 : {WeaponsSlots[1]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[1].EquippedWeapon);
                _currentWeaponEquipped = WeaponsSlots[1].EquippedWeapon;
            }
        }

        private void Number2Handler()
        {
            Debug.Log("Weapon slot 2 is empty!");
            if (WeaponsSlots[2] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 2 : {WeaponsSlots[2]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[2].EquippedWeapon);
                _currentWeaponEquipped = WeaponsSlots[2].EquippedWeapon;
            }
        }

        private void Number3Handler()
        {
            Debug.Log("Weapon slot 3 is empty!");
            if (WeaponsSlots[3] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 3 : {WeaponsSlots[3]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[3].EquippedWeapon);
                _currentWeaponEquipped = WeaponsSlots[3].EquippedWeapon;
            }
        }

        private void Number4Handler()
        {
            Debug.Log("Weapon slot 4 is empty!");
            if (WeaponsSlots[4] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 4 : {WeaponsSlots[4]} is equipped!");
                OnWeaponChanged?.Invoke(WeaponsSlots[4].EquippedWeapon);
                _currentWeaponEquipped = WeaponsSlots[4].EquippedWeapon;
            }
        }
        #endregion PRIVATE
    }
}