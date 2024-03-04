using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.BuffDebuffModule
{
    [Serializable]
    public class PoorTreatment : IBuffDebuffCommand
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
            Task.Run(() =>
            {
                for (int i = 0; i < LifeTime && !_cancellationTokenSource.IsCancellationRequested; i++)
                {
                    Task.Delay(1000, _cancellationTokenSource.Token).Wait();

                    if (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        buffDebuffController.CharacterController.CharacterDataController.CharacterData.HP += SingleImpact.AbsoluteValue;
                        _cachedValue += SingleImpact.AbsoluteValue;
                    }
                }
            });

            _cachedValue = 0;

            DeleteFromDictionary();
        }

        public void Destroy()
        {
            _cancellationTokenSource.Cancel();
            buffDebuffController.CharacterController.CharacterDataController.CharacterData.HP -= _cachedValue;
            _cachedValue = 0;

            DeleteFromDictionary();
        }

        public SingleImpact GetProcessedImpactValue(SingleImpact buffDebuff)
        {
            return buffDebuff;
        }

        private void DeleteFromDictionary()
        {
            foreach (var key in buffDebuffController.Buffs.Keys)
            {
                if (buffDebuffController.Buffs[key] is PoorTreatment)
                {
                    buffDebuffController.Buffs.Remove(key);
                    break;
                }
            }
        }
    }
}