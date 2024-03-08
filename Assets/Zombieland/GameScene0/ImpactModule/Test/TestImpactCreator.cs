using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule.Test
{
    public class TestImpactCreator : MonoBehaviour
    {
        public string FileName;
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
                
                DirectImpact = new SlowdownProjectile
                {
                    Detector = new UpfrontRayDetector
                    {
                        DetectionRadius = 0f
                    }, 
                    InitialImpactData = new List<DirectImpactData>
                    {
                        new DirectImpactData
                        {
                            Type = DirectImpactType.None,
                            AbsoluteValue = 0f,
                            PercentageValue = 0f
                        }
                    },
                    TargetReachedEffectPrefabID = "TargetReachedEffectPrefabID", 
                    NoTargetEffectPrefabID = "NoTargetEffectPrefabID"
                },
                
                BuffDebuffInjection = new BuffDebuffInjection
                {
                    Buffs = new List<IBuffDebuffCommand>
                    {
                        new Buff_WeakHealing
                        {
                            BuffDebuffData = new BuffDebuffData
                            {
                                ID = "ID",
                                Name = "Name",
                                IconID = "IconID",
                                PrefabID = "PrefabID",
                                VFXPosition = VFXPosition.Body,
                                LifeTime = 0,
                                Interval = 0,
                                DirectImpactData = new DirectImpactData
                                {
                                    Type = DirectImpactType.None,
                                    AbsoluteValue = 0f,
                                    PercentageValue = 0f
                                }
                            }
                        },
                    },
                    Debuffs = new List<IBuffDebuffCommand>
                    {
                        new Debuff_Slowdown()
                        {
                            BuffDebuffData = new BuffDebuffData
                            {
                                ID = "ID",
                                Name = "Name",
                                IconID = "IconID",
                                PrefabID = "PrefabID",
                                VFXPosition = VFXPosition.Body,
                                LifeTime = 0,
                                Interval = 0,
                                DirectImpactData = new DirectImpactData
                                {
                                    Type = DirectImpactType.None,
                                    AbsoluteValue = 0f,
                                    PercentageValue = 0f
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
