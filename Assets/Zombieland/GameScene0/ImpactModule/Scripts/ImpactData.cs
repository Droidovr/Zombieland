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
        [JsonIgnore] public Transform FollowTargetTransform { get; set; }
        [JsonIgnore] public Vector3 ObjectSpawnPosition { get; set; }
        [JsonIgnore] public Quaternion ObjectRotation { get; set; }
        [JsonIgnore] public List<Collider> IgnoringColliders { get; set; }
        [JsonIgnore] public GameObject ImpactObject { get; set; }

        public string ID { get; set; }
        public string Name { get; set; }
        public string IconID { get; set; }
        public List<ConsumableResource> ConsumableResources { get; set; }
    }
}