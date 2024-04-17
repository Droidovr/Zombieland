using System;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public event Action OnShotPerformed;

        private IWeaponController _weaponController;
        private FirePermiserTimer _firePermitionTimer;
        private InvokeTimer _cooldawnTimer;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResurser _weaponResurser;

        #region Public
        public void Init(IWeaponController weaponController)
        {
            _weaponController = weaponController;
            _firePermitionTimer = new FirePermiserTimer(_weaponController);
            _cooldawnTimer = new InvokeTimer(_weaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _impact = new Impact();
            _weaponImpacter = new WeaponImpacter(_weaponController);
            _weaponResurser = new WeaponResurser(_weaponController);
        }

        public void StartFire()
        {
            _firePermitionTimer.Start();
            _firePermitionTimer.OnPermission += PreparingFire;

            _impact = _weaponImpacter.GetCurrentImpact();

            Debug.Log("Старт стрельбы");
        }

        public void StopFire()
        {
            _firePermitionTimer?.Stop();
            _firePermitionTimer.OnPermission -= PreparingFire;

            _cooldawnTimer?.Stop();

            _weaponResurser.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
        }
        #endregion


        #region Private
        private void PreparingFire()
        {
            _firePermitionTimer?.Stop();
            _firePermitionTimer.OnPermission -= PreparingFire;

            _weaponResurser.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

            Debug.Log("Запуск стрельбы");

            _weaponController.CharacterController.AnimationController.OnFinishPreparationAttack += CompletionFire;
        }

        private void CompletionFire()
        {
            _weaponController.CharacterController.AnimationController.OnFinishPreparationAttack -= CompletionFire;

            _impact.Activate();
            _weaponResurser.IsReserveResurce = false;

            _weaponController.CharacterController.VisualBodyController.WeaponSoundFire?.Play();

            _weaponController.CharacterController.VisualBodyController.WeaponVFX?.Play();

            OnShotPerformed.Invoke();

            _cooldawnTimer.Start();
        }
        #endregion
    }
}