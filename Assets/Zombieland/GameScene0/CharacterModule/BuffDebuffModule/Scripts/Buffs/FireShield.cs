using System;
using System.Threading.Tasks;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    public class FireShield : IBuffDebuffCommand
    {
        public string Name { get; private set; }
        public ICharacterController ImpactTarget { get; private set; }
        public ICharacterController Owner { get; private set; }

        public float LifeTime = 20f;

        private BuffDebuffController _buffDebuffController;

        public FireShield(BuffDebuffController buffDebuffController)
        {
            _buffDebuffController = buffDebuffController;
        }

        public async void Execute()
        {
            await Task.Delay(TimeSpan.FromSeconds(LifeTime));

            _buffDebuffController.Buffs.Remove(Name);

            // ����� VFX - �������� ������ �� ����������� ��  VFX-����������� (������������� ������)
        }

        public BuffDebuff ProcessImpact(BuffDebuff buffDebuff)
        {
            throw new NotImplementedException();
        }
    }
}