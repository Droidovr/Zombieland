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
        public event Action OnShotAnimationPreparing;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        [JsonIgnore] public ICharacterController Owner { get; set; }

        public float TimeBetweenShots { get; set; }
        public float TimeBetweenRecharges { get; set; }

        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private ShotTimer _shotPermitionTimer;
        private InvokeTimer _preparingFireTimer;
        private InvokeTimer _completionFireTimer;
        private InvokeTimer _cooldawnTimer;
        private bool _isReservedResources;
        private Impact _impact;

        #region MainFireLogicScripts
        public void StartFire()
        {
            if (CheckFirePermission())
            {
                _shotPermitionTimer = new ShotTimer(CHECK_FIRE_PERMITION_PERIOD, CheckFirePermission);
                _shotPermitionTimer.Start();
                _shotPermitionTimer.OnPermission += StartPreparingFireTimer;
            }
            else
            {
                PreparingFire();
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
            // 1. Остановка таймера _shotPermitionTimer
            StopTimerSafely(_shotPermitionTimer);
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. Deserializator Impact. Filling Owner и Target. Target при этом нужно проганять через метод кучности выстрела. Target получаем от системы прицеливания
            _impact = new Deserializator().DeserializeImpact(Owner.WeaponController.CurrentImpactName);
            _impact.ImpactData.ImpactOwner = Owner;
            _impact.ImpactData.ObjectSpawnPosition = Owner.VisualBodyController.WeaponInScene.GetComponent<Transform>().TransformPoint(Owner.WeaponController.Weapon.WeaponData.FirePoint);
            _impact.ImpactData.ObjectRotation = Owner.VisualBodyController.WeaponInScene.GetComponent<Transform>().rotation; // направления брать из системы прицеливания и учитывать разброс оружия AddShotSpread(Target)


            // 3. Reserved Resources
            if (CheckFirePermission())
            {
                _isReservedResources = true;
                ResourceOperation(_isReservedResources);
            }

            // 4. Play Animation PreparingFire
            if (Owner.WeaponController.Weapon.WeaponData.AnimationPreparing != null)
            {
                OnShotAnimationPreparing.Invoke();
                StartCompletionFireTimer();
            }
            else
            {
                // 5. Fire 
                CompletionFire();
            }
        }

        private void CompletionFire()
        {
            StopTimerSafely(_completionFireTimer);

            //1. Not Reserved Resources
            _isReservedResources = false;

            //2. Impact Active
            _impact.Activate();

            //3. Play Sound
            //4. Instantiate FVX-shoot
            //5. Play Animation Weapon
            OnShotPerformed.Invoke();

            if (Owner.WeaponController.CurrentImpactCount <= 0)
            {
                OnAmmoDepleted.Invoke();
            }

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

        private void StartCompletionFireTimer()
        {
            _completionFireTimer = new InvokeTimer(Owner.WeaponController.Weapon.WeaponData.TimeAnimationPreparing, CompletionFire);
            _completionFireTimer.Start();
        }

        private void StartCooldownTimer()
        {
            _cooldawnTimer = new InvokeTimer(Owner.WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _cooldawnTimer.Start();
        }
        private bool CheckFirePermission()
        {
            // check the availability of resources or items, the absence of stun status, deaths, the presence of ammunition, the presence of a target, if provided.
            // Add after writing the Aiming module - is there a target?

            bool isCheckResource = ResourcesConsumption();
            bool isDead = Owner.CharacterDataController.CharacterData.IsDead;
            bool isStunned = Owner.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ResourcesConsumption()
        {
            bool isCheckResource = false;

            isCheckResource = Owner.WeaponController.CurrentImpactCount >= 0;

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
            Owner.WeaponController.CurrentImpactCount += isReservedResources ? -1 : 1;

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

        public Vector3 AddShotSpread(Vector3 targetPosition)
        {
            WeaponData weaponData = Owner.WeaponController.Weapon.WeaponData;

            float spreadX = UnityEngine.Random.Range(-weaponData.ShotAccuracy, weaponData.ShotAccuracy);
            float spreadY = UnityEngine.Random.Range(-weaponData.ShotAccuracy, weaponData.ShotAccuracy);
            float spreadZ = UnityEngine.Random.Range(-weaponData.ShotAccuracy, weaponData.ShotAccuracy);

            Vector3 shotSpread = new Vector3(spreadX, spreadY, spreadZ);

            return targetPosition + shotSpread;
        }
        #endregion
    }
}