using System;
using System.Timers;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class WeakTreatment : IBuffDebuffCommand
    {
        public string Name { get; private set; }
        public DirectImpactSetting DirectImpactSetting { get; private set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }

        public int LifeTime;
        public int Interval;

        private PeriodicAction _periodicAction;

        public void Execute()
        {
            _periodicAction = new PeriodicAction(LifeTime, Interval, IncreaseHP);
            _periodicAction.OnFinished += OnFinishedHandler;
            _periodicAction.Start();
        }

        public void Destroy()
        {
            _periodicAction.Stop();
        }

        public DirectImpactSetting GetProcessedImpactValue(DirectImpactSetting buffDebuff)
        {
            return buffDebuff;
        }

        private void OnFinishedHandler()
        {
            _periodicAction.OnFinished -= OnFinishedHandler;
            SelfDestroy();
        }

        private void SelfDestroy()
        {
            ImpactTarget.BuffDebuffController.Buffs.Remove(Name);
        }

        private void IncreaseHP(object sender, ElapsedEventArgs e)
        {
            var HP = ImpactTarget.CharacterDataController.CharacterData.HP + DirectImpactSetting.AbsoluteValue;

            if (HP <= ImpactTarget.CharacterDataController.CharacterData.HPMax)
            {
                ImpactTarget.CharacterDataController.CharacterData.HP = HP;
            }
            else
            {
                ImpactTarget.CharacterDataController.CharacterData.HP = ImpactTarget.CharacterDataController.CharacterData.HPMax;
            }
        }
    }
}