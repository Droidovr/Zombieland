using System;
using System.Timers;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class Slowdown : IBuffDebuffCommand
    {
        public BuffDebuffData BuffDebuffData { get; set; }

        private PeriodicAction _periodicAction;
        private float _chacheMaxMovingSpeed;

        public void Execute()
        {
            Debug.Log("Slowdown Execute");
            _chacheMaxMovingSpeed = BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed;
            BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed = _chacheMaxMovingSpeed * BuffDebuffData.DirectImpactData.PercentageValue / 100;

            _periodicAction = new PeriodicAction(BuffDebuffData.LifeTime, BuffDebuffData.Interval, DeSlowdown);
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
            BuffDebuffData.ImpactTarget.BuffDebuffController.Debuffs.Remove(BuffDebuffData.Name);
        }

        private void DeSlowdown(object sender, ElapsedEventArgs e)
        {
            Debug.Log("DeSlowdown");
            BuffDebuffData.ImpactTarget.CharacterDataController.CharacterData.MaxMovingSpeed = _chacheMaxMovingSpeed;
        }
    }
}