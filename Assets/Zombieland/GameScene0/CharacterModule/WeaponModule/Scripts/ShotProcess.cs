using System;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class ShotProcess : IShotProcess
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;

        [JsonIgnore] public ICharacterController Owner { get; set; }

        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private ShotTimer _shotPermitionTimer;
        private InvokeTimer _preparingFireTimer;
        private InvokeTimer _cooldawnTimer;
        private bool _isReservedResources;
        private Impact _impact;
        private Transform _target;

        #region MainFireLogicScripts
        public void StartFire()
        {
            if (CheckFirePermission())
            {
                PreparingFire();
            }
            else
            {
                _shotPermitionTimer = new ShotTimer(CHECK_FIRE_PERMITION_PERIOD, CheckFirePermission);
                _shotPermitionTimer.Start();
                _shotPermitionTimer.OnPermission += StartPreparingFireTimer;
            }
        }

        public void StopFire()
        {
            StopTimerSafely(_shotPermitionTimer);
            _shotPermitionTimer.OnPermission -= PreparingFire;

            StopTimerSafely(_preparingFireTimer);
            if (_isReservedResources)
            {
                ResourceOperation(_isReservedResources);
                _isReservedResources = false;
            }

            StopTimerSafely(_cooldawnTimer);
        }

        private void PreparingFire()
        {
            // 1. Stop Timer _shotPermitionTimer
            StopTimerSafely(_shotPermitionTimer);
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. Deserializator Impact.
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            _impact = Owner.RootController.GameDataController.GetData<Impact>(Owner.WeaponController.CurrentImpactName);

            _impact.ImpactData.ImpactOwner = Owner;

            if (Owner.WeaponController.Weapon.WeaponData.HasTarget)
            {
                _impact.ImpactData.FollowTargetTransform = _target;
            }

            _impact.ImpactData.ObjectSpawnPosition = Owner.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(Owner.VisualBodyController.WeaponPointFire.position);

            _impact.ImpactData.ObjectRotation = AddShotSpread(_target);
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 3. Reserved Resources
            if (CheckFirePermission())
            {
                _isReservedResources = true;
                ResourceOperation(_isReservedResources);
            }

            // 4. Subscribe to the event from the animation when to fire a shot
            Owner.AnimationController.OnFinishPreparationAttack += CompletionFire;
        }

        private void CompletionFire()
        {
            Owner.AnimationController.OnFinishPreparationAttack += CompletionFire;

            //1. Not Reserved Resources
            _isReservedResources = false;

            //2. Impact Active
            _impact.Activate();

            //3. Play Sound
            Owner.VisualBodyController.WeaponSoundFire?.Play();

            //4. Play FVX-shoot
            Owner.VisualBodyController.WeaponVFX?.Play();

            //5. We trigger the event that the shot was fired.
            OnShotPerformed.Invoke();

            if (Owner.WeaponController.CharacterController.EquipmentController.CurrentAmmoCount <= 0)
            {
                OnAmmoDepleted.Invoke();
            }

            _target = null;

            //6. Cooldown
            StartCooldownTimer();
        }
        #endregion



        #region HelperScripts
        private void StartPreparingFireTimer()
        {
            _preparingFireTimer = new InvokeTimer(0, PreparingFire);
            _preparingFireTimer.Start();
        }

        private void StartCooldownTimer()
        {
            _cooldawnTimer = new InvokeTimer(Owner.WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _cooldawnTimer.Start();
        }
        private bool CheckFirePermission()
        {
            // check the availability of resources or items, the absence of stun status, deaths, the presence of ammunition, the presence of a target, if provided.

            bool isCheckResource = ResourcesConsumption();
            bool isDead = Owner.CharacterDataController.CharacterData.IsDead;
            bool isStunned = Owner.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                if (Owner.WeaponController.Weapon.WeaponData.HasTarget)
                {
                    _target = Owner.AimingController.GetTarget();
                    if (_target != null)
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
            bool isCheckResource = Owner.WeaponController.CharacterController.EquipmentController.CurrentAmmoCount >= 0;

            for (int i = 0; i < _impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (_impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (_impact.ImpactData.ConsumableResources[i].Value >= Owner.CharacterDataController.CharacterData.Stamina)
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

        private void ResourceOperation(bool isReservedResources)
        {
            Owner.WeaponController.CharacterController.EquipmentController.CurrentAmmoCount += isReservedResources ? -1 : 1;

            for (int i = 0; i < _impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (_impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (isReservedResources)
                        {
                            Owner.CharacterDataController.CharacterData.Stamina -= _impact.ImpactData.ConsumableResources[i].Value;
                        }
                        else
                        {
                            Owner.CharacterDataController.CharacterData.Stamina += _impact.ImpactData.ConsumableResources[i].Value;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void StopTimerSafely(IFireTimer timer)
        {
            if (timer != null)
            {
                timer?.Stop();
            }
        }

        public Quaternion AddShotSpread(Transform target)
        {
            Quaternion finalRotation = new Quaternion();

            if (Owner.WeaponController.Weapon.WeaponData.HasTarget)
            {
                float shotAccuracy = Owner.WeaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = UnityEngine.Random.Range(-shotAccuracy, shotAccuracy);
                Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);
                
                Vector3 startPosition = Owner.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(Owner.VisualBodyController.WeaponPointFire.position);
                
                Vector3 directionToTarget = (target.position - startPosition).normalized;
                
                Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                finalRotation = deviationRotation * directionQuaternion;
            }
            else
            {
                float deviationAngle = UnityEngine.Random.Range(-Owner.WeaponController.Weapon.WeaponData.ShotAccuracy, Owner.WeaponController.Weapon.WeaponData.ShotAccuracy);
                finalRotation = Owner.VisualBodyController.WeaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
            }

            return finalRotation;
        }
        #endregion
    }
}