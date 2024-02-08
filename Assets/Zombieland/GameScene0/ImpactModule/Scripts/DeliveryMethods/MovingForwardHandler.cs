using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class MovingForwardHandler : IImpactCommand
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

        private Vector3 _objectSpawnPosition;

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
        }

        public void Execute()
        {
            _impactObject.SetActive(true);
            _objectSpawnPosition = _impactObject.transform.position;
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
            _impactObject.transform.Translate(Vector3.forward * (ProjectileSpeed * Time.deltaTime));
            if (Vector3.Distance(_objectSpawnPosition, _impactObject.transform.position) >= MaxDistance)
                ImpactController.Deactivate();
        }
    }
}