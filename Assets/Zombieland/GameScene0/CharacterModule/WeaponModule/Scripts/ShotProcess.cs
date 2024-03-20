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
            // ��������/���������� ���������� ��������: 
            //1.���� ��������
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

            //2.���������� ���� ���������� ��������� ���������� ��������.
        }

        private void PreparingFire()
        {
            // 1. ��������� ������� _shotPermitionTimer
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. ����������� �������, ������� ����� �����, ��� ���� �����, �� �� �������.
            // �) �������� � EquipmentSystem � ����� �������� ������
            // �) �� ������� �� ������ List<ConsumableResource> ConsumableResources ������� ���� �������

            // ������� ����� ������� � EquipmentSystem � ����� �������� ������
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

            // 3. ������ ���� �������.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // 4. ��������� ��� ���� ������ ��� ������. ��������� Owner � Targets. Targets ��� ���� ����� ��������� ����� ����� �������� ��������.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 5. ���� ���� �������� ������������ ���������� - ��������� �� ����������.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 6. �� ��������� ���� �������� ��������� Fire().
            CompletionFire();

        }

        private void CompletionFire()
        {
            //1. ������� ����������������� �������.
            isReservedResources = false;

            //2. ������� ���� ������� � ���������� ���.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //3. ����������� ���� ��������.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //4. ��������� FVX-�������� �� ����.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //5. ���� ���� �������� �������� ����������� ��.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //6. ������� ������� ����� ����� ShootCooldown �� StartFire().
            _invokeTimer = new InvokeTimer(Weapon.WeaponData.ShootCooldown, StartFire);
            _invokeTimer.Start();
        }
        #endregion



        #region HelperScripts
        private bool CheckFirePermission()
        {
            // ��������� ������� �������� ��� �����, ���������� ��������� �����, ������, ������� ��������, ������� ����, ���� ��� �������������.
            // �������� ����� ��������� ������ ���������� - ������� ��������
            // �������� ����� ��������� ������ ������������ - ���� �� ����

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