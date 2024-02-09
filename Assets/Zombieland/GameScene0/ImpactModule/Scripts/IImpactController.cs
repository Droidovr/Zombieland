using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactController
    {
        public Vector3 SpawnPosition { get; set; }
        public Transform TargetTransform { get; set; }
        public List<IImpactable> TargetImpactableList { get; set; }
        public ImpactData ImpactData { get; set; }
        public void Activate();
        public void Deactivate();
    }
}