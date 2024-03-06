using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IImpactable
    {
        public Transform ImpactObjectTransform { get; }
        public void ApplyImpact(IImpact impact);
    }
}
