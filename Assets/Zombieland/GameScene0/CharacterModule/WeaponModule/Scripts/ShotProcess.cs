using System;
namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class ShotProcess : IShotProcess
    {
        public IWeapon Weapon { get; set; }
        public IConsumption Consumption { get; set; }
        public float CheckFirePermissionPeriod { get; set; }

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
            return true;
        }

        private void ResourcesConsumption() // ������ ��������(��������� � �������)
        {
            // �������� ��� ������ ����� �������������� � ��������� � ����� CharacterData ��� ���� � ��� �����������.
        }

        private void Fire()
        { 
        
        }
    }
}