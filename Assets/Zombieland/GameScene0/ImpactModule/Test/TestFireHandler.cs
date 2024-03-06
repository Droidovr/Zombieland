using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class TestFireHandler : MonoBehaviour
    {
        public string ImpactName;
        public Transform SpawnPositionTransform;
        public Transform TargetTransform;
        public Transform MineSpawnPosition;
        [SerializeReference]
        public List<ImpactDetectionSensor> TargetImpactableList;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var textAsset = Resources.Load<TextAsset>(ImpactName);
                if (textAsset == null)
                    Debug.LogError("Cannot find file at " + ImpactName);
                var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
                var impact = JsonConvert.DeserializeObject<Impact>(textAsset.text, settings);
                if (impact.Delivery is MovingForwardHandler movingForwardHandler)
                {
                    movingForwardHandler.ObjectSpawnPosition = SpawnPositionTransform.position; 
                    movingForwardHandler.ObjectRotation = SpawnPositionTransform.rotation;
                    movingForwardHandler.IgnoringColliders = new List<Collider>{GetComponent<Collider>()};
                }
                else if (impact.Delivery is FollowingTargetHandler followingTargetHandler)
                {
                    followingTargetHandler.ObjectSpawnPosition = SpawnPositionTransform.position; 
                    followingTargetHandler.ObjectRotation = SpawnPositionTransform.rotation;
                    followingTargetHandler.TargetTransform = TargetTransform;
                    followingTargetHandler.IgnoringColliders = new List<Collider>{GetComponent<Collider>()};
                }
                else if (impact.Delivery is ObjectInstantTeleport objectInstantTeleport)
                {
                    objectInstantTeleport.ObjectSpawnPosition = MineSpawnPosition.position; 
                    objectInstantTeleport.ObjectRotation = Quaternion.identity;
                    objectInstantTeleport.IgnoringColliders = new List<Collider>{GetComponent<Collider>()};
                }
                
                impact.Activate();
            }
        }
    }
}
