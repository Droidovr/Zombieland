using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class DefaultImpact : InitialImpact
    {
        public string TargetEffectPrefabID { get; set; }
        public string NOTargetEffectPrefabID { get; set; }

        public override void Execute()
        {
            base.Execute();
            
            if (Impact.Targets == null)
            {
                var effectPrefab = Resources.Load<GameObject>(NOTargetEffectPrefabID);
                var effect = GameObject.Instantiate(effectPrefab, Impact.ImpactObject.transform.position, Quaternion.identity);
                var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                GameObject.Destroy(effect, effectTime);
                Impact.Deactivate();
            }
            else
            {
                var effectPrefab = Resources.Load<GameObject>(TargetEffectPrefabID);
                foreach (var target in Impact.Targets)
                {
                    target.Owner.TakeImpactController.ApplyImpact(InitialImpactData);
                    
                    var effect = GameObject.Instantiate(effectPrefab, target.Transform.position, Quaternion.identity);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
                Impact.BuffDebuffInjection.Execute();
            }
        }
    }
}
