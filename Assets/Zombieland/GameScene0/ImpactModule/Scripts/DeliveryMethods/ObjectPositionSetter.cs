using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectPositionSetter : IImpactCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string PrefabName { get; set; }

        public IDetectorCommand TargetObjectsDetector { get; set; }
        public List<IImpactCommand> ImpactsList { get; set; }
        
        private GameObject _impactObject;
        private CollisionHandler _collisionHandler;

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
        }
        
        public void Execute()
        {
            _impactObject.SetActive(true);
        }

        public void Deactivate()
        {
            _impactObject.SetActive(false);
        }
        
        private void ProcessCollision(Collider targetObjectCollider)
        {
            TargetObjectsDetector.TargetObjectCollider = targetObjectCollider;
            TargetObjectsDetector.Execute();
            // Impacts Execution
            ImpactController.Deactivate();
        }
    }
}
