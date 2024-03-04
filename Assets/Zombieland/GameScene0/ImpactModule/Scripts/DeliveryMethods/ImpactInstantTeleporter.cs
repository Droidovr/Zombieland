using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactInstantTeleporter : IDeliveryCommand
    {
        [JsonIgnore]
        public IImpactController ImpactController { get; set; }
        [JsonIgnore]
        public GameObject ImpactObject { get; set; }

        public List<IImpactCommand> ImpactsExecutionList { get; set; }

        public void Init()
        {
            foreach (var impact in ImpactsExecutionList)
            {
                impact.ImpactController = ImpactController;
                impact.Init();
            }
        }
        
        public void Execute()
        {
            ApplyImpactOnDelivery();
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
            // Has no implementation
        }
    }
}