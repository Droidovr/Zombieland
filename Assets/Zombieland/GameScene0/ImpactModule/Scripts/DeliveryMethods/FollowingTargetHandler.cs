using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class FollowingTargetHandler : IDeliveryCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string PrefabName { get; set; }
        public float MaxDistance { get; set; }
        public float ProjectileSpeed { get; set; }
        
        public IDetectorCommand Detector { get; set; }
        public List<IImpactCommand> ImpactsExecutionList{ get; set; }

        [JsonIgnore]
        public GameObject ImpactObject { get; set; }

        private Updater _updater;

        private Transform _targetTransform;
        private float _lifeTime;
        private float _currentTime;
        
        public void Init()
        {
            var impactObjectPrefab = Resources.Load<GameObject>(PrefabName);
            ImpactObject = GameObject.Instantiate(impactObjectPrefab);
            ImpactObject.SetActive(false);
            _updater = ImpactObject.AddComponent<Updater>();

            Detector.ImpactController = ImpactController;
            Detector.Init();

            foreach (var impact in ImpactsExecutionList)
            {
                impact.ImpactController = ImpactController;
                impact.Init();
            }
            
            _targetTransform = ImpactController.TargetTransform;
            _lifeTime = MaxDistance / ProjectileSpeed;
        }

        public void Activate()
        {
            ImpactObject.SetActive(true);
            Detector.Activate();
            _currentTime = _lifeTime;
            _updater.SubscribeToUpdate(MoveObject);
        }
        
        public void ApplyImpactOnDelivery()
        {
            foreach (var impact in ImpactsExecutionList)
            {
                impact.Activate();
            }
            ImpactController.Deactivate();
        }

        public void Deactivate()
        {
            ImpactObject.SetActive(false);
            _updater.UnsubscribeFromUpdate(MoveObject);
        }

        private void MoveObject()
        {
            ImpactObject.transform.position = Vector3.MoveTowards(ImpactObject.transform.position,
                _targetTransform.position, ProjectileSpeed * Time.deltaTime);
            
            _currentTime -= Time.deltaTime;
            if(_currentTime <= 0)
                ImpactController.Deactivate();
        }
    }
}
