using System;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public event Action OnShotPerformed;

        private IWeaponController _weaponController;
        private Impact _impact;
        private WeaponImpacter _weaponImpacter;
        private WeaponResourcer _weaponResourcer;

        public void Init(IWeaponController weaponController)
        {
            _weaponController = weaponController;
            _weaponImpacter = new WeaponImpacter(_weaponController);
            _weaponResourcer = new WeaponResourcer(_weaponController);
        }

        public void StartFire()
        {
            _impact = _weaponImpacter.GetCurrentImpact();

            _weaponResourcer.ResourceOperation(true, _impact.ImpactData.ConsumableResources);

            Debug.Log("WeaponPointFire in ShotProcess: " + _weaponController.WeaponPointFire.position);

            _impact.Activate();

            OnShotPerformed.Invoke();

            _weaponResourcer.IsReserveResurce = false;
        }

        public void StopFire()
        {
            if (_weaponResourcer.IsReserveResurce && _impact != null)
            {
                _weaponResourcer.ResourceOperation(false, _impact.ImpactData.ConsumableResources);
            }
        }
    }
}