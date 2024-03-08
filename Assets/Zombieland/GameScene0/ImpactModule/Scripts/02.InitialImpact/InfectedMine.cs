using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class InfectedMine : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public IDetector Detector { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }
        public float LifeTime { get; set; }
        public string ExplosionEffectPrefabID { get; set; }
        public string OnTargetEffectPrefabID { get; set; }

        private Updater _updater;

        public void Execute()
        {
            if (LifeTime <= 0)
            {
                ActivateMine();
            }
            else
            {
                _updater = Impact.ImpactObject.AddComponent<Updater>();
                _updater.SubscribeToUpdate(CheckLifetime);
            }
        }

        private void CheckLifetime()
        {
            LifeTime -= Time.deltaTime;
            if(LifeTime > 0) return;
            ActivateMine();
        }

        private void ActivateMine()
        {
            var explosionEffectPrefab = Resources.Load<GameObject>(ExplosionEffectPrefabID);
            var explosionEffect = GameObject.Instantiate(explosionEffectPrefab, Impact.ImpactObject.transform.position, Quaternion.identity);
            var explosionEffectTime = explosionEffect.GetComponent<ParticleSystem>().main.duration;
            GameObject.Destroy(explosionEffect, explosionEffectTime);
            
            var targetsList = Detector.GetTargets(Impact.ImpactObject);
            Impact.Targets = targetsList;

            if (targetsList != null)
            {
                var onTargetEffectPrefab = Resources.Load<GameObject>(OnTargetEffectPrefabID);
                foreach (var target in Impact.Targets)
                {
                    target.TestApplyDirectImpact(InitialImpactData);
                    //target.Owner.TakeImpactController.ApplyImpact(InitialImpactData);
                    if(!onTargetEffectPrefab) return;
                    var effect = GameObject.Instantiate(onTargetEffectPrefab, target.Transform.position, Quaternion.identity);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
            }
            Impact.BuffDebuffInjection.Execute();
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
