using System;
using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<int, WeaponSlot> WeaponSlots { get; private set; }
        //public Dictionary<string, int> CurrentImpactsEquipped { get; private set; }
        public int CurrentImpactCount 
        { 
            get { return CurrentImpactCount; } 
            set { if (value <= 0) return; else CurrentImpactCount = value; }
        }
        public string CurrentOutfitEquipped { get; private set; }

        private Weapon _currentWeaponEquipped;
        private string _currentImpactIDEquipped;
        private int _currentActiveSlotIndex;


        public EquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        public void ReloadCurrentWeapon()
        {
            if (_currentWeaponEquipped.WeaponData.MaxImpactCount == -1)
            {
                return;
            }
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped] == 0)
            {
                return;
            }
            int reloadAmount = _currentWeaponEquipped.WeaponData.MaxImpactCount - CurrentImpactCount;
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped] == reloadAmount)
            {
                return;
            }
            if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped] > reloadAmount)
            {
                CurrentImpactCount += reloadAmount;
                WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped] -= reloadAmount;
            }
            else
            {
                CurrentImpactCount += WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped];
                WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[_currentImpactIDEquipped] = 0;
            }
        }

        public override void Enable()
        {
            WeaponSlots = new Dictionary<int, WeaponSlot>() { { 1, WeaponSlot.empty }, { 2, WeaponSlot.empty }, { 3, WeaponSlot.empty }, { 4, WeaponSlot.empty } };

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
            CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 += Number4Handler;

            CharacterController.InventoryController.OnMainSlotEquipped += MainSlotEquippedHandler;
            CharacterController.InventoryController.OnCurrentImpactEquipped += CurrentImpactEquippedHandler;
            CharacterController.InventoryController.OnCurrentOutfitEquipped += CurrentOutfitEquippedHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED

        #region PRIVATE
        private void MainSlotEquippedHandler(string name, int slotNumber)
        {
            WeaponSlots[slotNumber].SetEquippedWeapon(CharacterController.RootController.GameDataController.GetData<Weapon>(name));
        }

        private void CurrentImpactEquippedHandler(string impactID, int amount)
        {
            if (_currentWeaponEquipped.WeaponData.AvailableImpactIDs.Contains(impactID))
            {
                if (WeaponSlots[_currentActiveSlotIndex].EquippedImpacts.ContainsKey(impactID))
                {
                    WeaponSlots[_currentActiveSlotIndex].EquippedImpacts[impactID] += amount;
                    return;
                }
                WeaponSlots[_currentActiveSlotIndex].AddEquippedImpact(impactID, amount);
                OnImpactChanged?.Invoke(impactID);
                _currentImpactIDEquipped = impactID; //Temporary logic, will be replaced after proper UI ability to choose impacts
                ReloadCurrentWeapon(); //Temporary logic, will be replaced after proper UI ability to choose impacts
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
            if (WeaponSlots[1] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 1 : {WeaponSlots[1]} is equipped! Shooting with {_currentImpactIDEquipped}");
                OnWeaponChanged?.Invoke(WeaponSlots[1].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[1].EquippedWeapon;
                OnImpactChanged?.Invoke(WeaponSlots[1].EquippedImpacts.Keys.First());
                _currentImpactIDEquipped = WeaponSlots[1].EquippedImpacts.Keys.First();
                _currentActiveSlotIndex = 1;
            }
        }

        private void Number2Handler()
        {
            Debug.Log("Weapon slot 2 is empty!");
            if (WeaponSlots[2] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 2 : {WeaponSlots[2]} is equipped! Shooting with {_currentImpactIDEquipped}");
                OnWeaponChanged?.Invoke(WeaponSlots[2].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[2].EquippedWeapon;
                OnImpactChanged?.Invoke(WeaponSlots[2].EquippedImpacts.Keys.First());
                _currentImpactIDEquipped = WeaponSlots[2].EquippedImpacts.Keys.First();
                _currentActiveSlotIndex = 2;
            }
        }

        private void Number3Handler()
        {
            Debug.Log("Weapon slot 3 is empty!");
            if (WeaponSlots[3] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 3 : {WeaponSlots[3]} is equipped! Shooting with  {_currentImpactIDEquipped}");
                OnWeaponChanged?.Invoke(WeaponSlots[3].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[3].EquippedWeapon;
                OnImpactChanged?.Invoke(WeaponSlots[3].EquippedImpacts.Keys.First());
                _currentImpactIDEquipped = WeaponSlots[3].EquippedImpacts.Keys.First();
                _currentActiveSlotIndex = 3;
            }
        }

        private void Number4Handler()
        {
            Debug.Log("Weapon slot 4 is empty!");
            if (WeaponSlots[4] != WeaponSlot.empty)
            {
                Debug.Log($"Weapon in slot 4 : {WeaponSlots[4]} is equipped! Shooting with  {_currentImpactIDEquipped}");
                OnWeaponChanged?.Invoke(WeaponSlots[4].EquippedWeapon);
                _currentWeaponEquipped = WeaponSlots[4].EquippedWeapon;
                OnImpactChanged?.Invoke(WeaponSlots[4].EquippedImpacts.Keys.First());
                _currentImpactIDEquipped = WeaponSlots[4].EquippedImpacts.Keys.First();
                _currentActiveSlotIndex = 4;
            }
        }
        #endregion PRIVATE
    }
}