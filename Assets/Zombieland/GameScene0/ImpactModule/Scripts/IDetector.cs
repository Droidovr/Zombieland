using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public interface IDetector
    {
        public List<IImpactable> GetTargets(GameObject impactObject);
    }
}
