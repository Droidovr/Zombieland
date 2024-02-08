using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class FollowTargetHandler : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string PrefabName { get; set; }
        public float MaxDistance { get; set; }
        public float ProjectileSpeed { get; set; }

        public IDetectorCommand TargetObjectsDetector { get; set; }
        public List<IImpactCommand> ImpactsList { get; set; }

        private GameObject _impactObject;
        private CollisionHandler _collisionHandler;
        private Updater _updater;

        private Transform _targetTransform;
        private float _lifeTime;
        private float _currentTime;
        
        public void Init()
        {
            var impactObjectPrefab = Resources.Load<GameObject>(PrefabName);
            _impactObject = GameObject.Instantiate(impactObjectPrefab);
            _impactObject.SetActive(false);
            
            TargetObjectsDetector.ImpactController = ImpactController;
            TargetObjectsDetector.ImpactObjectTransform = _impactObject.transform;

            foreach (var impact in ImpactsList)
            {
                impact.ImpactController = ImpactController;
                impact.Init();
            }
            
            _collisionHandler = _impactObject.AddComponent<CollisionHandler>();
            _collisionHandler.Init(ProcessCollision);
            _updater = _impactObject.AddComponent<Updater>();

            _targetTransform = ImpactController.TargetTransform;
            _lifeTime = MaxDistance / ProjectileSpeed;
        }

        public void Execute()
        {
            _impactObject.SetActive(true);
            _currentTime = _lifeTime;
            _updater.SubscribeToUpdate(MoveObject);
        }
        
        public void Deactivate()
        {
            _impactObject.SetActive(false);
            _updater.UnsubscribeFromUpdate(MoveObject);
        }

        private void ProcessCollision(Collider targetObjectCollider)
        {
            TargetObjectsDetector.TargetObjectCollider = targetObjectCollider;
            TargetObjectsDetector.Execute();
            // Impacts Execution
            ImpactController.Deactivate();
        }

        private void MoveObject()
        {
            _impactObject.transform.position = Vector3.MoveTowards(_impactObject.transform.position,
                _targetTransform.position, ProjectileSpeed * Time.deltaTime);
            
            _currentTime -= Time.deltaTime;
            if(_currentTime <= 0)
                ImpactController.Deactivate();
        }
    }
}
