using System;
using System.Timers;
using Zombieland.GameScene0.ImpactModule;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class FirePermiserTimer : IFireTimer
    {
        public event Action OnPermission;

        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private Timer _timer;
        private IWeaponController _weaponController;

        public FirePermiserTimer(IWeaponController weaponController)
        {
            _weaponController = weaponController;

            int intervalMS = (int)(CHECK_FIRE_PERMITION_PERIOD * 1000);
            _timer = new Timer(intervalMS);
            _timer.SynchronizingObject = null;
        }

        public void Start()
        {
            _timer.Elapsed += HandleTimerElapsed;
            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer.Elapsed -= HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //if (CheckFirePermission())
            //{
                OnPermission?.Invoke();
            //}
        }

        private bool CheckFirePermission()
        {
            bool isCheckResource = ResourcesConsumption();
            bool isDead = _weaponController.CharacterController.CharacterDataController.CharacterData.IsDead;
            bool isStunned = _weaponController.CharacterController.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                if (_weaponController.Weapon.WeaponData.HasTarget)
                {
                    if (_weaponController.CharacterController.AimingController.GetTarget() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ResourcesConsumption()
        {
            bool isCheckResource = _weaponController.CharacterController.EquipmentController.CurrentImpactCount >= 0;
            Impact impact = _weaponController.CharacterController.RootController.GameDataController.GetData<Impact>(_weaponController.CurrentImpactID);

            for (int i = 0; i < impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (impact.ImpactData.ConsumableResources[i].Value >= _weaponController.CharacterController.CharacterDataController.CharacterData.Stamina)
                        {
                            isCheckResource = true;
                        }
                        else
                        {
                            isCheckResource = false;
                        }
                        break;

                    default:
                        isCheckResource = false;
                        break;
                }
            }

            return isCheckResource;
        }
    }
}