using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class MinorHealing : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public List <DirectImpactData> InitialImpactData { get; set; }
        public string OnTargetEffectPrefabID { get; set; }

        public void Execute()
        {
            var effectPrefab = Resources.Load<GameObject>(OnTargetEffectPrefabID);
            foreach (var target in Impact.Targets)
            {
                target.TestApplyDirectImpact(InitialImpactData);
                //target.Owner.TakeImpactController.ApplyImpact(InitialImpactData);
                if(!effectPrefab) return;
                var effect = GameObject.Instantiate(effectPrefab, target.Transform.position, Quaternion.identity);
                var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                GameObject.Destroy(effect, effectTime);
            }
            Impact.BuffDebuffInjection.Execute();
        }
        
        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
