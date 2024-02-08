using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactController
    {
        public Vector3 TargetPosition { get; set; }
        public Transform TargetTransform { get; set; }
        public List<IImpactable> TargetImpactableList { get; set; }

        public void Activate();
        public void Activate(Vector3 targetPosition);
        public void Activate(Transform targetTransform);
        public void Activate(List<IImpactable> targetImpactableList);

        public void Deactivate();
    }
}