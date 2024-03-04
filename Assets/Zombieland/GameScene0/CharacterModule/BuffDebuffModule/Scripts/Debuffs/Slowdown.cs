using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class Slowdown : IBuffDebuffCommand
    {
        public string Name { get; private set; }
        public SingleImpact SingleImpact { get; private set; }
        public IBuffDebuffController buffDebuffController { get; set; }
        public ICharacterController ImpactTarget { get; set; }
        public ICharacterController Owner { get; set; }

        public float LifeTime;

        private float _cachedValue;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void Execute()
        {
            _cachedValue = buffDebuffController.CharacterController.CharacterDataController.CharacterData.MaxMovingSpeed * SingleImpact.PercentageValue / 100;
            buffDebuffController.CharacterController.CharacterDataController.CharacterData.MaxMovingSpeed -= _cachedValue;

            Task.Delay((int)LifeTime * 1000, _cancellationTokenSource.Token).ContinueWith(task =>
            {
                buffDebuffController.CharacterController.CharacterDataController.CharacterData.MaxMovingSpeed += _cachedValue;
            });

            _cachedValue = 0;

            DeleteFromDictionary();
        }

        public void Destroy()
        {
            _cancellationTokenSource.Cancel();
            buffDebuffController.CharacterController.CharacterDataController.CharacterData.MaxMovingSpeed += _cachedValue;

            _cachedValue = 0;

            DeleteFromDictionary();
        }

        public SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff)
        {
            return buffDebuff;
        }

        private void DeleteFromDictionary()
        {
            foreach (var key in buffDebuffController.Debuffs.Keys)
            {
                if (buffDebuffController.Debuffs[key] is Slowdown)
                {
                    buffDebuffController.Debuffs.Remove(key);
                    break;
                }
            }
        }
    }
}