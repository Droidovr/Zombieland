using System;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
 
        public ICharacterController CharacterController { get; set; }

        private FirePermiserTimer _shotPermitionTimer;
        private InvokeTimer _cooldawnTimer;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResurser _weaponResurser;

        #region PublicScripts
        public void Init()
        {
            _shotPermitionTimer = new FirePermiserTimer(CharacterController);
            _cooldawnTimer = new InvokeTimer(CharacterController.WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _weaponImpacter = new WeaponImpacter(CharacterController);
            _weaponResurser = new WeaponResurser(CharacterController);
        }

        public void StartFire()
        {
            _shotPermitionTimer.Start();
            _shotPermitionTimer.OnPermission += PreparingFire;
        }

        public void StopFire()
        {
            StopTimerSafely(_shotPermitionTimer);
            _shotPermitionTimer.OnPermission -= PreparingFire;

            StopTimerSafely(_cooldawnTimer);

            _weaponResurser.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
        }
        #endregion


        #region PrivateScripts
        private void PreparingFire()
        {
            StopTimerSafely(_shotPermitionTimer);
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _impact = _weaponImpacter.GetCurrentImpact();

            _weaponResurser.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

            CharacterController.AnimationController.OnFinishPreparationAttack += CompletionFire;
        }

        private void CompletionFire()
        {
            CharacterController.AnimationController.OnFinishPreparationAttack += CompletionFire;

            _impact.Activate();
            _weaponResurser.IsReserveResurce = false;

            CharacterController.VisualBodyController.WeaponSoundFire?.Play();

            CharacterController.VisualBodyController.WeaponVFX?.Play();

            OnShotPerformed.Invoke();

            if (CharacterController.WeaponController.CharacterController.EquipmentController.CurrentAmmoCount <= 0)
            {
                OnAmmoDepleted.Invoke();
            }

            _cooldawnTimer.Start();
        }

        private void StopTimerSafely(IFireTimer timer)
        {
            if (timer != null)
            {
                timer?.Stop();
            }
        }
        #endregion
    }
}