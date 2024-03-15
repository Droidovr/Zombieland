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
            //1.Проверка возможности стрельбы прямо сейчас CheckFirePermission()
            //  a) Если запрещено -мы ждем периодически оценивая ситуацию пока вызоветься метод StopFire() либо можно будет стрелять.
            //     (запустить CheckFirePermissionTimer на проверку возможности стрельбы.По таймеру проверяем CheckFirePermission(), period 0,1с.)
            //  б) Если разрешено -Запускаем FireProceses().

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
            // плановое/намеренное прерывания стрельбы:
            //1.всех таймеров
            _shotPermitionTimer.OnShotPermition -= FireProceses;
            _shotPermitionTimer.Stop();

            //2.прерывание всех запущенных процессов касательно выстрела.
        }

        private void FireProceses()
        {
            //1.Останавливаем таймер CheckFirePermissionTimer(CheckFirePermission())
            _shotPermitionTimer.OnShotPermition -= FireProceses;
            _shotPermitionTimer.Stop();

            //2.Запуск таймера на подготовку(каст заклинания) = CheckFinishPreparation()
        }

        private void CheckFinishPreparation()
        {

        }

        private void FireInteruption()
        {
            // метод принудительного прерывания стрельбы (сбит каст заклинания, наложили стан, убили)
        }

        private bool CheckFirePermission()
        {
            // проверяем наличие ресурсов, отсутствие состояние стана, смерти
            return true;
        }

        private void ResourcesConsumption() // расход ресурсов(атрибутов и айтемов)
        {
            // Прогнать наш ресурс через бафдебафсистем и проверить в нашей CharacterData или есть у нас возможность.
        }
    }
}