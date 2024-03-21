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
            // ��������/���������� ���������� ��������: 
            //1.���� ��������
            _shotPermitionTimer?.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            _invokeTimer?.Stop();

            if (_isReservedResources)
            {
                _isReservedResources = false;
            }

            //2.���������� ���� ���������� ��������� ���������� ��������.
        }

        private void PreparingFire()
        {
            // 1. ��������� ������� _shotPermitionTimer
            _shotPermitionTimer.Stop();
            _shotPermitionTimer.OnPermission -= PreparingFire;

            // 2. ��������������� ������ � ��������� ����. ��������� ��� ���� ������ ��� ������. ��������� Owner � Targets. Targets ��� ���� ����� ��������� ����� ����� �������� ��������.
            _impact = new Deserializator().DeserializeImpact(Owner.WeaponController.CurrentImpactName);
            _impact.ImpactData.ImpactOwner = Owner;


            // 3. ����������� �������, ������� ����� �����, ��� ���� �����, �� �� �������.
            // �) �������� � EquipmentSystem � ����� �������� ������
            // �) �� ������� �� ������ List<ConsumableResource> ConsumableResources ������� ���� �������

            // ������� ����� ������� � EquipmentSystem � ����� �������� ������
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

            // 4. ���� ���� �������� ������������ ���������� - ��������� �� ����������.
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // 5. �� ��������� ���� �������� ��������� Fire().
            CompletionFire();

        }

        private void CompletionFire()
        {
            OnShotPerformed.Invoke();

            //1. ������� ����������������� �������.
            _isReservedResources = false;

            //2. ���������� ������.
            _impact.Activate();

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
            _invokeTimer = new InvokeTimer(Owner.WeaponController.Weapon.WeaponData.ShootCooldown, StartFire);
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