using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class TestFireHandler : MonoBehaviour
    {
        public string ImpactName;
        public Vector3 SpawnPosition;
        public Transform TargetTransform;
        [SerializeReference]
        public List<IImpactable> TargetImpactableList;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var impactController = new ImpactController(ImpactName, null);
                impactController.SpawnPosition = SpawnPosition;
                impactController.TargetTransform = TargetTransform;
                impactController.TargetImpactableList = TargetImpactableList;
                impactController.Activate();
            }
        }
    }
}
