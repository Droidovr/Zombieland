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
            //1.ѕроверка возможности стрельбы пр€мо сейчас CheckFirePermission()
            //  a) ≈сли запрещено -мы ждем периодически оценива€ ситуацию пока вызоветьс€ метод StopFire() либо можно будет стрел€ть.
            //     (запустить CheckFirePermissionTimer на проверку возможности стрельбы.ѕо таймеру провер€ем CheckFirePermission(), period 0,1с.)
            //  б) ≈сли разрешено -«апускаем FireProceses().

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
            // плановое/намеренное прерывани€ стрельбы: 
            //1.всех таймеров
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.прерывание всех запущенных процессов касательно выстрела.
        }

        private void FireProceses()
        {
            //1.ќстанавливаем таймер CheckFirePermissionTimer(CheckFirePermission())
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.«апуск таймера на подготовку(каст заклинани€) = CheckFinishPreparation()
            _shotFinishPreparationTimer = new ShotTimer(CheckFirePermissionPeriod, CheckFinishPreparation);
            _shotFinishPreparationTimer.Start();
        }

        private bool CheckFinishPreparation()
        {
            return true;
        }

        private void FireInteruption()
        {
            // метод принудительного прерывани€ стрельбы (сбит каст заклинани€, наложили стан, убили)
        }

        private bool CheckFirePermission()
        {
            // провер€ем наличие ресурсов или итемы, отсутствие состо€ние стана, смерти, наличие патронов, наличие цели, если это предусмотрено.
            
            bool isCheckResource = false;

            string currentImpactName = Weapon.WeaponData.Owner.WeaponController.CurrentImpactName;
            TestImpact currentImpact = Weapon.WeaponData.Owner.WeaponController.Impacts[currentImpactName];

            List<TestConsumableResource> consumableResource = currentImpact.ImpactData.ConsumableResources;
            for (int i = 0; i < consumableResource.Count; i++)
            {
                // ѕровер€ем наши ресурсы, если ресурсов больше или равно что нам нужно из consumableResource - возвращем true иначе false
                // Item, // кристалы, свитки и т.д.
                // Attributes // манна, здоровье, сила и т.д.

                isCheckResource = true;
            }
            // ѕодобно такому же провер€ем все другие услови€: состо€ние стана, смерти, наличие патронов, наличие цели, если это предусмотрено

            if (isCheckResource)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void ResourcesConsumption() // расход ресурсов(атрибутов и айтемов)
        {
            // ѕрогнать наш ресурс через бафдебафсистем и проверить в нашей CharacterData или есть у нас возможность.
        }

        private void Fire()
        { 
        
        }
    }
}