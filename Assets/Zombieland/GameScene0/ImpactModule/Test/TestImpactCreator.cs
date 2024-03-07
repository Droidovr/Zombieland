using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule.Test
{
    public class TestImpactCreator : MonoBehaviour
    {
        public Impact Impact;

        void Start()
        {
            Impact = new Impact
            {
                Assembler = new ObjectAssembler
                {
                    PrefabID = "PrefabID"
                },
                
                Delivery = new MovingForwardHandler
                {
                    MovingSpeed = 0f, 
                    Range = 0f, 
                    Lifetime = 0f
                },
                
                DirectImpact = new DefaultImpact{
                    Detector = new UpfrontRayDetector
                    {
                        DetectionRadius = 0f
                    }, 
                    InitialImpactData = new List<DirectImpactData>
                    {
                        new DirectImpactData
                        {
                            Type = DirectImpactType.NotType,
                            AbsoluteValue = 0f,
                            PercentageValue = 0f
                        }
                    },
                    TargetEffectPrefabID = "TargetEffectPrefabID", 
                    NOTargetEffectPrefabID = "NOTargetEffectPrefabID"
                },
                
                BuffDebuffInjection = new BuffDebuffInjection
                {
                    Buffs = new List<IBuffDebuffCommand>(),
                    Debuffs = new List<IBuffDebuffCommand>
                    {
                        new Slowdown
                        {
                            BuffDebuffData = new BuffDebuffData
                            {
                                
                            }
                        }
                    }
                }
            };
        }
    }
}
