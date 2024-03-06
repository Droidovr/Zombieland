using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class FollowingTargetHandler : IImpactCommand
    {
        public float MovingSpeed { get; set; }
        public float Range { get; set; }
        public float Lifetime { get; set; }

        [JsonIgnore] public IImpact Impact { get; set; }
        [JsonIgnore] public Vector3 ObjectSpawnPosition { get; set; }
        [JsonIgnore] public Quaternion ObjectRotation { get; set; }
        [JsonIgnore] public Transform TargetTransform { get; set; }
        [JsonIgnore] public List<Collider> IgnoringColliders { get; set; }

        private GameObject _impactObject;
        private Updater _updater;
        private bool _isLifetimeRelated;
        
        private const float ProjectileRotationSpeed = 50f;
        
        public void Execute()
        {
            _impactObject = ((ObjectAssembler) Impact.Assembler).ImpactObject;
            _impactObject.transform.position = ObjectSpawnPosition;
            _impactObject.transform.rotation = ObjectRotation;

            var collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            collisionHandler.Init(FinalizeDelivery, IgnoringColliders);
            
            _updater = _impactObject.AddComponent<Updater>();
            _updater.SubscribeToUpdate(MoveObject);

            _isLifetimeRelated = Range <= 0;
        }

        private void MoveObject()
        {
            _impactObject.transform.position = Vector3.MoveTowards(_impactObject.transform.position,
                TargetTransform.position, MovingSpeed * Time.deltaTime);

            var direction = TargetTransform.position - _impactObject.transform.position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _impactObject.transform.rotation = Quaternion.Lerp(_impactObject.transform.rotation, targetRotation,
                ProjectileRotationSpeed * Time.deltaTime);

            if(_isLifetimeRelated)
                CheckLifetime();
            else
                CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_impactObject.transform.position, ObjectSpawnPosition) < Range) return;
            FinalizeDelivery();
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
            _updater?.UnsubscribeFromUpdate(MoveObject);
        }
    }
}
