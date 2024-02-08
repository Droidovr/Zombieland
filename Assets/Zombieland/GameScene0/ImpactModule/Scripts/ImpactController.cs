using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.ImpactModule
{
    
    public class ImpactController : IImpactController
    {
        public Vector3 TargetPosition { get; set; }
        public Transform TargetTransform { get; set; }
        public List<IImpactable> TargetImpactableList { get; set; }

        private readonly IRootController _rootController;
        private readonly ImpactData _impactData;

        public ImpactController(string impactName, IRootController rootController)
        {
            _rootController = rootController;
            _impactData = _rootController.GameDataController.GetData<ImpactData>(impactName);
            _impactData.DeliveryHandler.ImpactController = this;
            _impactData.DeliveryHandler.Init();
        }

        /// <summary>
        /// Use for projectiles, that are moving straight forward
        /// </summary>
        public void Activate()
        {
            
        }
        
        /// <summary>
        /// Use for objects, that should appear at the certain position
        /// </summary>
        public void Activate(Vector3 targetPosition)
        {
            
        }
        
        /// <summary>
        /// Use for projectiles, that should follow the target
        /// </summary>
        public void Activate(Transform targetTransform)
        {
            
        }
        
        /// <summary>
        /// Use to apply instant impact on the selected targets
        /// </summary>
        public void Activate(List<IImpactable> targetImpactableList)
        {
            
        }

        public void Deactivate()
        {
            
        }
    }
}
