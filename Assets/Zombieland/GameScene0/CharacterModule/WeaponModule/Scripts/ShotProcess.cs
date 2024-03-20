using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Progress;
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
        private ShotTimer _shotFinishPreparationTimer;

        public void StartFire()
        {
            //1.Проверка возможности стрельбы прямо сейчас CheckFirePermission()
            //  a) Если запрещено -мы ждем периодически оценивая ситуацию пока вызоветься метод StopFire() либо можно будет стрелять.
            //     (запустить CheckFirePermissionTimer на проверку возможности стрельбы.По таймеру проверяем CheckFirePermission(), period 0,1с.)
            //  б) Если разрешено -Запускаем FireProceses().

            bool isFirePermition = CheckFirePermission();

            if (isFirePermition)
            {
                _shotPermitionTimer = new ShotTimer(CheckFirePermissionPeriod, CheckFirePermission);
                _shotPermitionTimer.Start();
                _shotPermitionTimer.OnPermission += FireProceses;
            }
            else
            {
                FireProceses();
            }
        }

        public void StopFire()
        {
            // плановое/намеренное прерывания стрельбы: 
            //1.всех таймеров
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.прерывание всех запущенных процессов касательно выстрела.
        }

        private void FireProceses()
        {
            //1.Останавливаем таймер CheckFirePermissionTimer(CheckFirePermission())
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.Запуск таймера на подготовку(каст заклинания) = CheckFinishPreparation()
            _shotFinishPreparationTimer = new ShotTimer(CheckFirePermissionPeriod, CheckFinishPreparation);
            _shotFinishPreparationTimer.Start();
        }

        private bool CheckFinishPreparation()
        {
            return true;
        }

        private void FireInteruption()
        {
            // метод принудительного прерывания стрельбы (сбит каст заклинания, наложили стан, убили)
        }

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

            string currentImpactName = Weapon.WeaponData.Owner.WeaponController.CurrentImpactName;
            TestImpact currentImpact = Weapon.WeaponData.Owner.WeaponController.Impacts[currentImpactName];
            List<TestConsumableResource> consumableResource = currentImpact.ImpactData.ConsumableResources;

            for (int i = 0; i < consumableResource.Count; i++)
            {
                switch (consumableResource[i].ResourceType)
                {
                    case TestResourceType.Stamina:
                        if (consumableResource[i].Value >= Weapon.WeaponData.Owner.WeaponController.CharacterController.CharacterDataController.CharacterData.Stamina)
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

        private void Fire()
        {

        }
    }
}