using System;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public event Action OnShotPerformed;
 
        public IWeaponController WeaponController { get; set; }

        private FirePermiserTimer _shotPermitionTimer;
        private InvokeTimer _cooldawnTimer;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResurser _weaponResurser;

        #region PublicScripts
        public void Init(IWeaponController weaponController)
        {
            _shotPermitionTimer = new FirePermiserTimer(WeaponController);
            _cooldawnTimer = new InvokeTimer(WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _weaponImpacter = new WeaponImpacter(WeaponController);
            _weaponResurser = new WeaponResurser(WeaponController);
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


        #region PrivateScripts
        private void PreparingFire()
        {
            _shotPermitionTimer?.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _impact = _weaponImpacter.GetCurrentImpact();

            _weaponResurser.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

            WeaponController.CharacterController.AnimationController.OnFinishPreparationAttack += CompletionFire;
        }

        private void CompletionFire()
        {
            WeaponController.CharacterController.AnimationController.OnFinishPreparationAttack -= CompletionFire;

            _impact.Activate();
            _weaponResurser.IsReserveResurce = false;

            WeaponController.CharacterController.VisualBodyController.WeaponSoundFire?.Play();

            WeaponController.CharacterController.VisualBodyController.WeaponVFX?.Play();

            OnShotPerformed.Invoke();

            _cooldawnTimer.Start();
        }
        #endregion
    }
}