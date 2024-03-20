using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.ImpactModule
{
    [Serializable]
    public class ImpactData
    {
        [JsonIgnore] public ICharacterController ImpactOwner { get; set; }
        [JsonIgnore] public List<IImpactable> Targets { get; set; }
        [JsonIgnore] public GameObject ImpactObject { get; set; }
        
        public List<ResourceType> ConsumableResources { get; set; }
    }
}
