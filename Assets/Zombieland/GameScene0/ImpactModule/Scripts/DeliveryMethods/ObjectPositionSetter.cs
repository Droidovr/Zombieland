using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ObjectPositionSetter : IDeliveryCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        [JsonIgnore]
        public GameObject ImpactObject { get; set; }
        public string PrefabName { get; set; }

        public IImpactCommand Detector { get; set; }
        public List<IImpactCommand> ImpactsExecutionList { get; set; }

        public void Init()
        {
            var impactObjectPrefab = Resources.Load<GameObject>(PrefabName);
            ImpactObject = GameObject.Instantiate(impactObjectPrefab);
            ImpactObject.SetActive(false);
            
            Detector.ImpactController = ImpactController;
            Detector.Init();

            foreach (var impact in ImpactsExecutionList)
            {
                impact.ImpactController = ImpactController;
                impact.Init();
            }
        }
        
        public void Activate()
        {
            ImpactObject.transform.position = ImpactController.SpawnPosition;
            ImpactObject.SetActive(true);
            Detector.Activate();
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
        }
    }
}
