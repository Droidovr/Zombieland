using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IDetectorCommand : ICommand
    {
        IImpactController ImpactController { get; set; }
        public Transform ImpactObjectTransform { set; }
        public Collider TargetObjectCollider { set; }
    }
}
