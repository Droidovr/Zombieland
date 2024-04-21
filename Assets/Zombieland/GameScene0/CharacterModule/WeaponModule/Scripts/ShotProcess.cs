using System;
using System.Threading;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : MonoBehaviour, IShotProcess
    {
        public event Action OnShotPerformed;

        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private IWeaponController _weaponController;
        private FirePermiser _firePermiser;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResourcer _weaponResourcer;

        #region Public
        public void Init(IWeaponController weaponController)
        {
            Debug.Log("Init - Thread ID: " + Thread.CurrentThread.ManagedThreadId);

            _weaponController = weaponController;
            _firePermiser = new FirePermiser(_weaponController);
            _weaponImpacter = new WeaponImpacter(_weaponController);
            _weaponResourcer = new WeaponResourcer(_weaponController);
        }

        public void StartFire()
        {
            Debug.Log("StartFire - Thread ID: " + Thread.CurrentThread.ManagedThreadId);

            _impact = _weaponImpacter.GetCurrentImpact();
            InvokeRepeating("StartFirePermision", 0, CHECK_FIRE_PERMITION_PERIOD);
        }

        public void StopFire()
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                CancelInvoke();
            }

            if (_weaponResourcer.IsReserveResurce && _impact != null)
            {
                _weaponResourcer.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
            }
        }
        #endregion


        #region Private
        private void PreparingFire()
        {
            Debug.Log("PreparingFire - Thread ID: " + Thread.CurrentThread.ManagedThreadId);

            _weaponResourcer.ResourceOperation(true, _impact.ImpactData.ConsumableResources);
            _weaponController.CharacterController.AnimationController.OnAnimationAttack += CompletionFire;
        }

        private void CompletionFire()
        {
            Debug.Log("CompletionFire - Thread ID: " + Thread.CurrentThread.ManagedThreadId);

            _weaponController.CharacterController.AnimationController.OnAnimationAttack -= CompletionFire;

            _impact.Activate();
            _weaponResourcer.IsReserveResurce = false;

            //_weaponController.CharacterController.VisualBodyController.WeaponSoundFire?.Play();

            //_weaponController.CharacterController.VisualBodyController.WeaponVFX?.Play();

            OnShotPerformed.Invoke();

            Invoke("StartFire", _weaponController.Weapon.WeaponData.ShootCooldown);
        }

        private void StartFirePermision()
        {
            Debug.Log("StartFirePermision - Thread ID: " + Thread.CurrentThread.ManagedThreadId);

            if (_firePermiser.CheckFirePermission(_impact))
            {
                CancelInvoke("StartFirePermision");
                PreparingFire();
            }
        }
        #endregion
    }
}