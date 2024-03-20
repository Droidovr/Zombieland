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
            //1.�������� ����������� �������� ����� ������ CheckFirePermission()
            //  a) ���� ��������� -�� ���� ������������ �������� �������� ���� ���������� ����� StopFire() ���� ����� ����� ��������.
            //     (��������� CheckFirePermissionTimer �� �������� ����������� ��������.�� ������� ��������� CheckFirePermission(), period 0,1�.)
            //  �) ���� ��������� -��������� FireProceses().

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
            // ��������/���������� ���������� ��������: 
            //1.���� ��������
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.���������� ���� ���������� ��������� ���������� ��������.
        }

        private void FireProceses()
        {
            //1.������������� ������ CheckFirePermissionTimer(CheckFirePermission())
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= FireProceses;

            //2.������ ������� �� ����������(���� ����������) = CheckFinishPreparation()
            _shotFinishPreparationTimer = new ShotTimer(CheckFirePermissionPeriod, CheckFinishPreparation);
            _shotFinishPreparationTimer.Start();
        }

        private bool CheckFinishPreparation()
        {
            return true;
        }

        private void FireInteruption()
        {
            // ����� ��������������� ���������� �������� (���� ���� ����������, �������� ����, �����)
        }

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