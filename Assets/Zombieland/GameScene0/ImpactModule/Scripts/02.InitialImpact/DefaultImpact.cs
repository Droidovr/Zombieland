using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class DefaultImpact : InitialImpact
    {
        public float Points { get; set; }
        public string OnTargetEffectPrefabName { get; set; }
        public string NoTargetEffectPrefabName { get; set; }

        protected override void ExecuteNoTargets()
        {
            Debug.Log("No Targets");
            base.ExecuteNoTargets();
            var effectPrefab = Resources.Load<GameObject>(NoTargetEffectPrefabName);
            GameObject.Instantiate(effectPrefab, impactObject.transform.position, Quaternion.identity);
            Impact.Deactivate();
        }

        protected override void ExecuteTargetsSet()
        {
            var effectPrefab = Resources.Load<GameObject>(OnTargetEffectPrefabName);

            foreach (var target in targets)
            {
                GameObject.Instantiate(effectPrefab, target.ImpactObjectTransform.position, Quaternion.identity);
            }
            ((BuffDebuffInjection) Impact.BuffDebuffInjection).Targets = targets;
            Impact.BuffDebuffInjection.Execute();
        }
    }
}
