using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectAssembler : IImpactCommand
    {
        public string PrefabName { get; set; }
        
        [JsonIgnore] public IImpact Impact { get; set; }
        [JsonIgnore] public GameObject ImpactObject { get; private set; }

        public void Execute()
        {
            var prefab = Resources.Load<GameObject>(PrefabName);
            ImpactObject = GameObject.Instantiate(prefab);
            
            Impact.Delivery.Execute();
        }

        public void Deactivate()
        {
            GameObject.Destroy(ImpactObject);
        }
    }
}
