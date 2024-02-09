using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.ImpactModule
{
    
    public class ImpactController : IImpactController
    {
        public Vector3 SpawnPosition { get; set; }
        public Transform TargetTransform { get; set; }
        public List<IImpactable> TargetImpactableList { get; set; }
        public ImpactData ImpactData { get; set; }

        private readonly IRootController _rootController;

        public ImpactController(string impactName, IRootController rootController)
        {
            _rootController = rootController;
            ImpactData = _rootController.GameDataController.GetData<ImpactData>(impactName);
            ImpactData.DeliveryHandler.ImpactController = this;
            ImpactData.DeliveryHandler.Init();
        }

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }
    }
}
