using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.SensorModule
{
    public class ImpactSensor : MonoBehaviour, IImpactable
    {
        public ICharacterController Owner { get; set; }
        public Transform Transform => transform;
        
        public void TestApplyDirectImpact(List<DirectImpactData> directImpactDataList)
        {
            foreach (var directImpact in directImpactDataList)
            {
                Debug.Log($"directImpact - {directImpact.Type}, points - {directImpact.AbsoluteValue}");
            }
        }

        public void TestApplyBuffs(List<IBuffDebuffCommand> buffs)
        {
            foreach (var buff in buffs)
            {
                Debug.Log($"buff - {buff.BuffDebuffData.Name}, time - {buff.BuffDebuffData.LifeTime}");
            }        
        }

        public void TestApplyDebuffs(List<IBuffDebuffCommand> debuffs)
        {
            foreach (var debuff in debuffs)
            {
                Debug.Log($"debuff - {debuff.BuffDebuffData.Name}, time - {debuff.BuffDebuffData.LifeTime}");
            }           
        }
    }
}