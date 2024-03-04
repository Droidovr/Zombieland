using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class MovingForwardHandler : IDeliveryCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        public string PrefabName { get; set; }
        public float MaxDistance { get; set; }
        public float ProjectileSpeed { get; set; }

        public IImpactCommand Detector { get; set; }
        public List<IImpactCommand> ImpactsExecutionList{ get; set; }
        
        [JsonIgnore]
        public GameObject ImpactObject { get; set; }
        
        private Updater _updater;

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
        }

        public void Execute()
        {
            ImpactObject.transform.position = ImpactController.SpawnPosition;
            ImpactObject.transform.rotation = ImpactController.InitialRotation;
            ImpactObject.SetActive(true);
            Detector.Execute();
            _updater.SubscribeToUpdate(MoveObject);
        }
        
        public void ApplyImpactOnDelivery()
        {
            foreach (var impact in ImpactsExecutionList)
            {
                impact.Execute();
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
            ImpactObject.transform.Translate(Vector3.forward * (ProjectileSpeed * Time.deltaTime));
            if (Vector3.Distance(ImpactController.SpawnPosition, ImpactObject.transform.position) >= MaxDistance)
                ImpactController.Deactivate();
        }
    }
}