using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectInstantTeleport : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }
        [JsonIgnore] public Vector3 ObjectSpawnPosition { get; set; }
        [JsonIgnore] public Quaternion ObjectRotation { get; set; }
        [JsonIgnore] public List<Collider> IgnoringColliders { get; set; }

        public void Execute()
        {
            Impact.ImpactObject.transform.position = ObjectSpawnPosition;
            Impact.ImpactObject.transform.rotation = ObjectRotation;

            var collisionHandler = Impact.ImpactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery, IgnoringColliders);
        }

        private void FinalizeDelivery()
        {
            Deactivate();
            Impact.DirectImpact.Execute();    
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
