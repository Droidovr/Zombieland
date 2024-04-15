using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectInstantTeleport : IImpactCommand
    {
        [JsonIgnore] public IImpact Impact { get; set; }

        public void Execute()
        {
            Impact.ImpactData.ImpactObject.transform.position = Impact.ImpactData.ObjectSpawnPosition;
            Impact.ImpactData.ImpactObject.transform.rotation = Impact.ImpactData.ObjectRotation;

            var collisionHandler = Impact.ImpactData.ImpactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery, Impact.ImpactData.IgnoringColliders);
        }

        private void FinalizeDelivery()
        {
            Deactivate();
            Impact.InitialImpact.Execute();    
        }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
