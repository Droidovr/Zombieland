using System;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public event Action OnShotPerformed;

        private IWeaponController _weaponController;
        private FirePermiserTimer _shotPermitionTimer;
        private InvokeTimer _cooldawnTimer;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResurser _weaponResurser;

        #region Public
        public void Init(IWeaponController weaponController)
        {
            _weaponController = weaponController;
            _shotPermitionTimer = new FirePermiserTimer(_weaponController);
            _cooldawnTimer = new InvokeTimer(_weaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _impact = new Impact();
            _weaponImpacter = new WeaponImpacter(_weaponController);
            _weaponResurser = new WeaponResurser(_weaponController);
        }

        public void StartFire()
        {
            _shotPermitionTimer.Start();
            _shotPermitionTimer.OnPermission += PreparingFire;
        }

        public void StopFire()
        {
            _shotPermitionTimer?.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _cooldawnTimer?.Stop();

            _weaponResurser.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
        }
        #endregion


        #region Private
        private void PreparingFire()
        {
            _shotPermitionTimer?.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _impact = _weaponImpacter.GetCurrentImpact();

            _weaponResurser.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

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