using System;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class WeakTreatment : IBuffDebuffCommand
    {
        public BuffDebuffData BuffDebuffData { get; set; }

        private PeriodicAction _periodicAction;

        public void Execute()
        {
            Debug.Log("WeakTreatment Execute");
            _periodicAction = new PeriodicAction(BuffDebuffData.LifeTime, BuffDebuffData.Interval, IncreaseHP);
            _periodicAction.OnFinished += OnFinishedHandler;
            _periodicAction.Start();
        }

        public void Destroy()
        {
            _periodicAction.Stop();
        }

        public DirectImpactData GetProcessedImpactValue(DirectImpactData buffDebuff)
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
            BuffDebuffData.ImpactTarget.BuffDebuffController.Buffs.Remove(BuffDebuffData.Name);
        }

        private void IncreaseHP(object sender, ElapsedEventArgs e)
        {
            Debug.Log("IncreaseHP");
            var HP = BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.HP + BuffDebuffData.DirectImpactData.AbsoluteValue;

            if (HP <= BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.HPMax)
            {
                BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.HP = HP;
            }
            else
            {
                BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.HP = BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.HPMax;
            }
        }
    }
}