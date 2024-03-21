using Newtonsoft.Json;
using System;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class ShotProcess : IShotProcess
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        [JsonIgnore] public ICharacterController Owner { get; set; }

        public float TimeBetweenShots { get; set; }
        public float TimeBetweenRecharges { get; set; }

        private const float CHECK_FIRE_PERMITION_PERIOD = 0.1f;

        private ShotTimer _shotPermitionTimer;
        private InvokeTimer _invokeTimer;
        private bool _isReservedResources;
        private Impact _impact;

        #region MainFireLogicScripts
        public void StartFire()
        {
            if (CheckFirePermission())
            {
                _shotPermitionTimer = new ShotTimer(CHECK_FIRE_PERMITION_PERIOD, CheckFirePermission);
                _shotPermitionTimer.Start();
                _shotPermitionTimer.OnPermission += PreparingFire;
            }
            else
            {
                PreparingFire();
            }
        }

        public void StopFire()
        {
            // плановое/намеренное прерывания стрельбы: 
            //1.всех таймеров
            _shotPermitionTimer?.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _invokeTimer?.Stop();

            if (_isReservedResources)
            {
                _isReservedResources = false;
            }

            //2.прерывание всех запущенных процессов касательно выстрела.
        }

        private void PreparingFire()
        {
            // 1. Остановка таймера _shotPermitionTimer
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. Десериализируем Импакт и заполняем поля. Заполнить все поля нужные для Импакт. Установка Owner и Targets. Targets при этом нужно проганять через метод кучности выстрела.
            _impact = new Deserializator().DeserializeImpact(Owner.WeaponController.CurrentImpactName);
            _impact.ImpactData.ImpactOwner = Owner;


            // 3. Резервируем ресурсы, которые нужно снять, при этом снять, но не тратить.
            // а) Минусуем в EquipmentSystem в нашем магазине Импакт
            // б) Из Импакта по списку List<ConsumableResource> ConsumableResources снимаем наши ресурсы

            // Здесьну нужно списать у EquipmentSystem в нашем магазине Импакт
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (CheckFirePermission())
            {
                for (int i = 0; i < _impact.ImpactData.ConsumableResources.Count; i++)
                {
                    switch (_impact.ImpactData.ConsumableResources[i].ResourceType)
                    {
                        case ResourceType.Stamina:
                            Owner.CharacterDataController.CharacterData.Stamina -= _impact.ImpactData.ConsumableResources[i].Value;
                            break;

                        default:
                            break;
                    }
                }

                _isReservedResources = true;
            }

            // 4. Если есть анимация проигрывания подготовки - дождаться ее завершения.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 5. По окончании всех процедур запускаем Fire().
            CompletionFire();

        }

        private void CompletionFire()
        {
            OnShotPerformed.Invoke();

            //1. Снимаем зерезервированные ресурсы.
            _isReservedResources = false;

            //2. Активируем Импакт.
            _impact.Activate();

            //3. Проигрываем звук выстрела.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //4. Запускаем FVX-выстрела из дула.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //5. Если есть анимация выстрела проигрываем ее.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //6. Переход обратно через время ShootCooldown на StartFire().
            _invokeTimer = new InvokeTimer(Owner.WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
            _invokeTimer.Start();
        }
        #endregion



        #region HelperScripts
        private bool CheckFirePermission()
        {
            // проверяем наличие ресурсов или итемы, отсутствие состояние стана, смерти, наличие патронов, наличие цели, если это предусмотрено.
            // Дописать после написания модуля Экипировки - наличие патронов
            // Дописать после написания модуля Прицеливания - есть ли цель

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
        #endregion
    }
}