using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public class EquipmentController : Controller, IEquipmentController
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action OnEquipmentChanged;
        public event Action<string> OnImpactChanged;
        public event Action OnAmmoDepleted;

        public ICharacterController CharacterController { get; private set; }
        public List<Weapon> WeaponsSlots { get; private set; }
        public int CurrentImpactCount { get; set; }

        public EquipmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
            WeaponsSlots = new List<Weapon>();
        }

        public void ReloadCurrentWeapon()
        {

        }

        public override void Disable()
        {
            CharacterController.RootController.UIController.OnNumber1 -= Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 -= Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 -= Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 -= Number4Handler;

            base.Disable();
        }

        public void PickUpWeapon(Weapon weapon)
        {
            //WeaponsSlots.Add(weapon);
        }

        #region PROTECTED
        protected override void CreateHelpersScripts()
        {
            WeaponsSlots.Add(CharacterController.RootController.GameDataController.GetData<Weapon>("Wrench_0"));
            WeaponsSlots.Add(CharacterController.RootController.GameDataController.GetData<Weapon>("Pistol_0"));
            WeaponsSlots.Add(CharacterController.RootController.GameDataController.GetData<Weapon>("AK_0"));

            CharacterController.RootController.UIController.OnNumber1 += Number1Handler;
            CharacterController.RootController.UIController.OnNumber2 += Number2Handler;
            CharacterController.RootController.UIController.OnNumber3 += Number3Handler;
            CharacterController.RootController.UIController.OnNumber4 += Number4Handler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn't have any subsystems at the moment.
        }
        #endregion PROTECTED

        #region PRIVATE
        private void Number1Handler()
        {
            Debug.Log("Weapon in slot 1 is equipped!");

            OnWeaponChanged?.Invoke(WeaponsSlots[0]);
            OnImpactChanged?.Invoke(WeaponsSlots[0].WeaponData.AvailableImpactIDs[0]);
            CurrentImpactCount = 10000;
        }

        private void Number2Handler()
        {
            Debug.Log("Weapon in slot 2 is equipped!");

            OnWeaponChanged?.Invoke(WeaponsSlots[1]);
            OnImpactChanged?.Invoke(WeaponsSlots[1].WeaponData.AvailableImpactIDs[0]);
            CurrentImpactCount = 10000;
        }

        private void Number3Handler()
        {
            Debug.Log("Weapon in slot 3 is equipped!");

            OnWeaponChanged?.Invoke(WeaponsSlots[2]);
            OnImpactChanged?.Invoke(WeaponsSlots[2].WeaponData.AvailableImpactIDs[0]);
            CurrentImpactCount = 10000;
        }

        private void Number4Handler()
        {
            //Debug.Log("Weapon in slot 4 is equipped!");
            //if (WeaponsSlots[3] != null)
            //{
            //    OnWeaponChanged?.Invoke(WeaponsSlots[3]);
            //}
        }
        #endregion PRIVATE
    }
}