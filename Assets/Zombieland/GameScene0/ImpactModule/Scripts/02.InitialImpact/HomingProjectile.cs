using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class HomingProjectile : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public IDetector Detector { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }
        public string TargetReachedEffectPrefabID { get; set; }
        public string NoTargetEffectPrefabID { get; set; }

        public void Execute()
        {
            var targetsList = Detector.GetTargets(Impact.ImpactObject);
            Impact.Targets = targetsList;
            
            if (targetsList == null || targetsList.Count <= 0)
            {
                var effectPrefab = Resources.Load<GameObject>(NoTargetEffectPrefabID);
                if (effectPrefab)
                {
                    var effect = GameObject.Instantiate(effectPrefab, Impact.ImpactObject.transform.position, Quaternion.identity);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
                Impact.Deactivate();
            }
            else
            {
                var effectPrefab = Resources.Load<GameObject>(TargetReachedEffectPrefabID);
                foreach (var target in Impact.Targets)
                {
                    //target.TestApplyDirectImpact(InitialImpactData);
                    target.Owner.TakeImpactController.ApplyImpact(InitialImpactData);
                    if(!effectPrefab) return;
                    var effect = GameObject.Instantiate(effectPrefab, Impact.ImpactObject.transform.position, Quaternion.identity);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
                Impact.BuffDebuffInjection.Execute();
            }
        }
        
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
