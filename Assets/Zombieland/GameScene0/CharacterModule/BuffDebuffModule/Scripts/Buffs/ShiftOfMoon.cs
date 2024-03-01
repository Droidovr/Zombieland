using System;
using System.Threading.Tasks;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class ShiftOfMoon : IBuffDebuffCommand
    {
        public string Name { get; private set; }
        public ICharacterController ImpactTarget { get; private set; }
        public ICharacterController Owner { get; private set; }

        public float LifeTime = 20f;

        private BuffDebuffController _buffDebuffController;

        public ShiftOfMoon(BuffDebuffController buffDebuffController)
        {
            _buffDebuffController = buffDebuffController;
        }

        public async void Execute()
        {
            await Task.Delay(TimeSpan.FromSeconds(LifeTime));

            _buffDebuffController.Buffs.Remove(Name);

            // ����� VFX - �������� ������ �� ����������� ��  VFX-����������� (������������� ������)
        }

        public SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff)
        {
            switch (buffDebuff.Type)
            {
                case SingleImpactType.Fire:
                    // Fire - ������� ������������ ���������
                    return buffDebuff;

                case SingleImpactType.Poison:
                    // Poison
                    return buffDebuff;

                default:
                    // ������� �������� ���������
                    return buffDebuff;
            }
        }
    }
}