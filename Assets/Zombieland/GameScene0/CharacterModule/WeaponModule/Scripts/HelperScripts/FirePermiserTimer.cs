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
        private ICharacterController _characterController;
        private Impact _impact;

        public FirePermiserTimer(ICharacterController characterController)
        {
            _characterController = characterController;

            int intervalMS = (int)(CHECK_FIRE_PERMITION_PERIOD * 1000);
            _timer = new Timer(intervalMS);

            _impact = _characterController.RootController.GameDataController.GetData<Impact>(_characterController.WeaponController.CurrentImpactName);
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
            if (CheckFirePermission())
            {
                OnPermission?.Invoke();
            }
        }

        private bool CheckFirePermission()
        {
            bool isCheckResource = ResourcesConsumption();
            bool isDead = _characterController.CharacterDataController.CharacterData.IsDead;
            bool isStunned = _characterController.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                if (_characterController.WeaponController.Weapon.WeaponData.HasTarget)
                {
                    if (_characterController.AimingController.GetTarget() != null)
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
            bool isCheckResource = _characterController.WeaponController.CharacterController.EquipmentController.CurrentAmmoCount >= 0;

            for (int i = 0; i < _impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (_impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (_impact.ImpactData.ConsumableResources[i].Value >= _characterController.CharacterDataController.CharacterData.Stamina)
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