using System.Timers;
namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class ShotProcess : IShotProcess
    {
        public IWeapon Weapon { get; set; }
        public IConsumption Consumption { get; set; }
        public float CheckFirePermissionPeriod { get; set; }

        private ShotPermitionTimer _shotPermitionTimer;

        public void StartFire()
        {
            //1.�������� ����������� �������� ����� ������ CheckFirePermission()
            //  a) ���� ��������� -�� ���� ������������ �������� �������� ���� ���������� ����� StopFire() ���� ����� ����� ��������.
            //     (��������� CheckFirePermissionTimer �� �������� ����������� ��������.�� ������� ��������� CheckFirePermission(), period 0,1�.)
            //  �) ���� ��������� -��������� FireProceses().

            bool isFirePermition = CheckFirePermission();

            if (isFirePermition)
            {
                _shotPermitionTimer = new ShotPermitionTimer(CheckFirePermissionPeriod, CheckFirePermission);
                _shotPermitionTimer.OnShotPermition += FireProceses;
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
            _shotPermitionTimer.OnShotPermition -= FireProceses;
            _shotPermitionTimer.Stop();

            //2.���������� ���� ���������� ��������� ���������� ��������.
        }

        private void FireProceses()
        {
            //1.������������� ������ CheckFirePermissionTimer(CheckFirePermission())
            _shotPermitionTimer.OnShotPermition -= FireProceses;
            _shotPermitionTimer.Stop();

            //2.������ ������� �� ����������(���� ����������) = CheckFinishPreparation()
        }

        private void CheckFinishPreparation()
        {

        }

        private void FireInteruption()
        {
            // ����� ��������������� ���������� �������� (���� ���� ����������, �������� ����, �����)
        }

        private bool CheckFirePermission()
        {
            // ��������� ������� ��������, ���������� ��������� �����, ������
            return true;
        }

        private void ResourcesConsumption() // ������ ��������(��������� � �������)
        {
            // �������� ��� ������ ����� �������������� � ��������� � ����� CharacterData ��� ���� � ��� �����������.
        }
    }
}