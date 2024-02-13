using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule;

namespace Zombieland.GameScene0.ImpactModule
{
    public class TestFireHandler : MonoBehaviour
    {
        public string ImpactName;
        public Transform SpawnPositionTransform;
        public Transform TargetTransform;
        [SerializeReference]
        public List<ImpactDetectionSensor> TargetImpactableList;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var impactController = new ImpactController(ImpactName, null);
                impactController.SpawnPosition = SpawnPositionTransform.position;
                impactController.InitialRotation = SpawnPositionTransform.rotation;
                impactController.TargetTransform = TargetTransform;
                if (TargetImpactableList.Count != 0)
                {
                    var list = TargetImpactableList.Cast<IImpactable>().ToList();
                    impactController.TargetImpactableList = list;
                }
                
                impactController.Activate();
            }
        }
    }
}
