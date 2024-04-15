using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.ImpactModule;

public class GunBullet : IInitialImpactCommand
{
    [JsonIgnore] public IImpact Impact { get; set; }
    public UpfrontRayDetector Detector { get; set; }
    public List <DirectImpactData> InitialImpactData { get; set; }
    public string TargetReachedEffectPrefabName { get; set; }
    public string NoTargetEffectPrefabName { get; set; }
    public float Force { get; set; }

    public void Execute()
    {
        var targetsList = Detector.GetTargets(Impact.ImpactData.ImpactObject);
        Impact.ImpactData.Targets = targetsList;
            
        if (targetsList == null || targetsList.Count <= 0)
        {
            var effectPrefab = Resources.Load<GameObject>(NoTargetEffectPrefabName);
            if (effectPrefab)
            {
                var effect = GameObject.Instantiate(effectPrefab, Impact.ImpactData.ImpactObject.transform.position, Quaternion.identity);
                var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                GameObject.Destroy(effect, effectTime);
            }
            Impact.Deactivate();
        }
        else
        {
            var effectPrefab = Resources.Load<GameObject>(TargetReachedEffectPrefabName);
            foreach (var target in Impact.ImpactData.Targets)
            {
                if (target.Controller is ICharacterController characterController)
                {
                    characterController.TakeImpactController.ApplyImpact(InitialImpactData);
                    // target - ApplyForce
                    if(!effectPrefab) return;
                    var effect = GameObject.Instantiate(effectPrefab, Impact.ImpactData.ImpactObject.transform.position, Quaternion.identity);
                    var effectTime = effect.GetComponent<ParticleSystem>().main.duration;
                    GameObject.Destroy(effect, effectTime);
                }
            }
            Impact.BuffDebuffInjection.Execute();
        }
    }
        
    public void Deactivate()
    {
        // Has no implementation
    }
}
