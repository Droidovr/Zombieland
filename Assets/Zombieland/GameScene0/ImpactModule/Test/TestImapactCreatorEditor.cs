using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;

namespace Zombieland.GameScene0.ImpactModule.Test
{
    [CustomEditor(typeof(TestImpactCreator))]
    public class TestImapactCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TestImpactCreator testImpactCreator = (TestImpactCreator)target;

            if (GUILayout.Button("Save ImpactData"))
            {
                var fileName = "Test.txt";
                var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/ImpactModule/Resources", fileName);
                Debug.Log($"Save data to filepath: {filePath}");
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
                var json = JsonConvert.SerializeObject(GetImpact(), settings);
                File.WriteAllText(filePath, json);
            }
        } 
        private Impact GetImpact()
        {
            var Impact = new Impact
            {
                Assembler = new ImpactAssembler()
                {
                },
                
                Delivery = new ImpactInstantTeleport()
                {
                },
                
                InitialImpact = new MinorHealing()
                {
                    InitialImpactData = new List<DirectImpactData>
                    {
                        new DirectImpactData
                        {
                            Type = DirectImpactType.None,
                            AbsoluteValue = 15f,
                            PercentageValue = 0f
                        }
                    },
                    OnTargetEffectPrefabID = "HealingOnTargetEffect"
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
                                VFXPosition = VFXPosition.None,
                                LifeTime = 0f,
                                Interval = 0f,
                                DirectImpactData = new DirectImpactData
                                {
                                    Type = DirectImpactType.None,
                                    AbsoluteValue = 0f,
                                    PercentageValue = 0f
                                }
                            }
                        }
                    },
                    Debuffs = new List<IBuffDebuffCommand>
                    {
                    }
                }
            };
            
            return Impact;
        }
    }
}
