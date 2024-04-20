using System;
using System.Threading;
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
        private WeaponResourcer _weaponResourcer;

        #region Public
        public void Init(IWeaponController weaponController)
        {
            _weaponController = weaponController;
            _firePermitionTimer = new FirePermiserTimer(_weaponController);
            _cooldawnTimer = new InvokeTimer(_weaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _impact = new Impact();
            _weaponImpacter = new WeaponImpacter(_weaponController);
            _weaponResourcer = new WeaponResourcer(_weaponController);

            Debug.Log("ID Init Thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        public void StartFire()
        {
            _firePermitionTimer.OnPermission += PreparingFire;
            _firePermitionTimer.Start();

            _impact = _weaponImpacter.GetCurrentImpact();

            Debug.Log("ID Start Thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        public void StopFire()
        {
            Debug.Log("ID Stop Thread: " + Thread.CurrentThread.ManagedThreadId);
            _firePermitionTimer?.Stop();
            _firePermitionTimer.OnPermission -= PreparingFire;

            _cooldawnTimer?.Stop();

            if (_weaponResourcer.IsReserveResurce)
            {
                _weaponResourcer.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
            }
        }
        #endregion


        #region Private
        private void PreparingFire()
        {
            Debug.Log("ID Preparing Thread: " + Thread.CurrentThread.ManagedThreadId);

            _firePermitionTimer.OnPermission -= PreparingFire;

            _weaponResourcer.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

            _weaponController.CharacterController.AnimationController.OnFinishPreparationAttack += CompletionFire;

            //CompletionFire();
        }

        private void CompletionFire()
        {
            Debug.Log("ID Completion Thread: " + Thread.CurrentThread.ManagedThreadId);

            _weaponController.CharacterController.AnimationController.OnFinishPreparationAttack -= CompletionFire;

            _impact.Activate();
            _weaponResourcer.IsReserveResurce = false;

            //_weaponController.CharacterController.VisualBodyController.WeaponSoundFire?.Play();

            //_weaponController.CharacterController.VisualBodyController.WeaponVFX?.Play();

            OnShotPerformed.Invoke();

            _cooldawnTimer.Start();
        }
        #endregion
    }
}