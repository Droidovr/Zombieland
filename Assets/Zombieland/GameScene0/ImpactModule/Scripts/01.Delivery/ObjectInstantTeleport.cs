using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectInstantTeleport : IImpactCommand
    {
        public float Lifetime { get; set; }

        [JsonIgnore] public IImpact Impact { get; set; }
        [JsonIgnore] public Vector3 ObjectSpawnPosition { get; set; }
        [JsonIgnore] public Quaternion ObjectRotation { get; set; }
        [JsonIgnore] public List<Collider> IgnoringColliders { get; set; }
        
        private GameObject _impactObject;
        private Updater _updater;

        public void Execute()
        {
            _impactObject = ((ObjectAssembler) Impact.Assembler).ImpactObject;
            _impactObject.transform.position = ObjectSpawnPosition;
            _impactObject.transform.rotation = ObjectRotation;

            var collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery, IgnoringColliders);
            
            if(Lifetime <= 0) return;
            _updater = _impactObject.AddComponent<Updater>();
            _updater.SubscribeToUpdate(CheckLifetime);
        }
        
        private void CheckLifetime()
        {
            Lifetime -= Time.deltaTime;
            if(Lifetime > 0) return;
            FinalizeDelivery();
        }
        
        private void FinalizeDelivery()
        {
            Deactivate();
            Impact.DirectImpact.Execute();    
        }

        public void Deactivate()
        {
            _updater?.UnsubscribeFromUpdate(CheckLifetime);
        }
    }
}
