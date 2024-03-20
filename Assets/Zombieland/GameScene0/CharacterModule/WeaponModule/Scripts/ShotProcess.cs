using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class ShotProcess : IShotProcess
    {
        public IWeapon Weapon { get; set; }
        public IConsumption Consumption { get; set; }
        public float CheckFirePermissionPeriod { get; set; }
        public float TimeBetweenShots { get; set; }
        public float TimeBetweenRecharges { get; set; }

        private ShotTimer _shotPermitionTimer;
        private InvokeTimer _invokeTimer;
        private bool isReservedResources;

        #region MainFireLogicScripts
        public void StartFire()
        {
            if (CheckFirePermission())
            {
                _shotPermitionTimer = new ShotTimer(CheckFirePermissionPeriod, CheckFirePermission);
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

            if (isReservedResources)
            {
                ICharacterController owner = Weapon.WeaponData.Owner;

                string currentImpactName = owner.WeaponController.CurrentImpactName;
                TestImpact currentImpact = owner.WeaponController.Impacts[currentImpactName];
                List<TestConsumableResource> consumableResource = currentImpact.ImpactData.ConsumableResources;

                for (int i = 0; i < consumableResource.Count; i++)
                {
                    switch (consumableResource[i].ResourceType)
                    {
                        case TestResourceType.Stamina:
                            owner.WeaponController.CharacterController.CharacterDataController.CharacterData.Stamina += consumableResource[i].Value;
                            break;

                        default:
                            break;
                    }
                }

                isReservedResources = false;
            }

            //2.прерывание всех запущенных процессов касательно выстрела.
        }

        private void PreparingFire()
        {
            // 1. Остановка таймера _shotPermitionTimer
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. Резервируем ресурсы, которые нужно снять, при этом снять, но не тратить.
            // а) Минусуем в EquipmentSystem в нашем магазине Импакт
            // б) Из Импакта по списку List<ConsumableResource> ConsumableResources снимаем наши ресурсы

            // Здесьну нужно списать у EquipmentSystem в нашем магазине Импакт
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (CheckFirePermission())
            {
                ICharacterController owner = Weapon.WeaponData.Owner;

                string currentImpactName = owner.WeaponController.CurrentImpactName;
                TestImpact currentImpact = owner.WeaponController.Impacts[currentImpactName];
                List<TestConsumableResource> consumableResource = currentImpact.ImpactData.ConsumableResources;

                for (int i = 0; i < consumableResource.Count; i++)
                {
                    switch (consumableResource[i].ResourceType)
                    {
                        case TestResourceType.Stamina:
                            owner.WeaponController.CharacterController.CharacterDataController.CharacterData.Stamina -= consumableResource[i].Value;
                            break;

                        default:
                            break;
                    }
                }

                isReservedResources = true;
            }

            // 3. Делаем клон Импакта.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // 4. Заполнить все поля нужные для Импакт. Установка Owner и Targets. Targets при этом нужно проганять через метод кучности выстрела.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 5. Если есть анимация проигрывания подготовки - дождаться ее завершения.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 6. По окончании всех процедур запускаем Fire().
            CompletionFire();

        }

        private void CompletionFire()
        {
            //1. Снимаем зерезервированные ресурсы.
            isReservedResources = false;

            //2. Сделать клон Импакта и активируем его.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            _invokeTimer = new InvokeTimer(Weapon.WeaponData.ShootCooldown, StartFire);
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
            bool isDead = Weapon.WeaponData.Owner.WeaponController.CharacterController.CharacterDataController.CharacterData.IsDead;
            bool isStunned = Weapon.WeaponData.Owner.WeaponController.CharacterController.CharacterDataController.CharacterData.IsStunned;

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

            IWeaponController weaponController = Weapon.WeaponData.Owner.WeaponController;

            string currentImpactName = weaponController.CurrentImpactName;
            TestImpact currentImpact = weaponController.Impacts[currentImpactName];
            List<TestConsumableResource> consumableResource = currentImpact.ImpactData.ConsumableResources;

            for (int i = 0; i < consumableResource.Count; i++)
            {
                switch (consumableResource[i].ResourceType)
                {
                    case TestResourceType.Stamina:
                        if (consumableResource[i].Value >= weaponController.CharacterController.CharacterDataController.CharacterData.Stamina)
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