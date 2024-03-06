using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class InitialImpact : IImpactCommand
    {
        public IDetector Detector { get; set; }

        [JsonIgnore] public IImpact Impact { get; set; }

        protected GameObject impactObject;
        protected List<IImpactable> targets;

        public void Execute()
        {
            impactObject = ((ObjectAssembler) Impact.Assembler).ImpactObject;
            var targetsList = Detector.GetTargets(impactObject);

            if (targetsList == null)
            {
                ExecuteNoTargets();
            }
            else
            {
                targets = targetsList;
                ExecuteTargetsSet();
            }
        }
        
        protected virtual void ExecuteTargetsSet() { }
        
        protected virtual void ExecuteNoTargets() { }

        public void Deactivate()
        {
            // Has no implementation
        }
    }
}
