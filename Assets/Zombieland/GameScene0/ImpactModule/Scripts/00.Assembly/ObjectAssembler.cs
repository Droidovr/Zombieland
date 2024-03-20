using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectAssembler : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        public string PrefabID { get; set; }

        public void Execute()
        {
            var impactObjectPrefab = Resources.Load<GameObject>(PrefabID);
            Impact.ImpactData.ImpactObject = GameObject.Instantiate(impactObjectPrefab);
            
            Impact.Delivery.Execute();
        }

        public void Deactivate()
        {
            GameObject.Destroy(Impact.ImpactData.ImpactObject);
        }
    }
}
