using System;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class Slowdown : IBuffDebuffCommand
    {
        public string Name { get; set; }
        public DirectImpactSetting DirectImpactSetting { get; set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }

        public int LifeTime;
        public int Interval;

        private PeriodicAction _periodicAction;
        private float _chacheMaxMovingSpeed;

        public void Execute()
        {
            Debug.Log("Slowdown Execute");
            _chacheMaxMovingSpeed = ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed;
            ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed = _chacheMaxMovingSpeed * DirectImpactSetting.PercentageValue / 100;

            _periodicAction = new PeriodicAction(LifeTime, Interval, DeSlowdown);
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
            ImpactTarget.BuffDebuffController.Debuffs.Remove(Name);
        }

        private void DeSlowdown(object sender, ElapsedEventArgs e)
        {
            Debug.Log("DeSlowdown");
            ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed = _chacheMaxMovingSpeed;
        }
    }
}