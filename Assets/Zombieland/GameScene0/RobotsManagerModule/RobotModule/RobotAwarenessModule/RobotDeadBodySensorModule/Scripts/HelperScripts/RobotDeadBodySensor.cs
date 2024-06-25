using System;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public class RobotDeadBodySensor : MonoBehaviour
    {
        public event Action<IController> OnDeadBodyDetected;

        private const float DETECTION_RANGE = 10.0f;
        private const float CHECK_INTERVAL = 0.2f;


        public void Init()
        {
            InvokeRepeating(nameof(DetectDeadBody), 0, CHECK_INTERVAL);
        }

        private void DetectDeadBody()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, DETECTION_RANGE);

            foreach (var hitCollider in hitColliders)
            {
                Impactable[] impactables = hitCollider.GetComponentsInChildren<Impactable>();
                if (impactables == null || impactables.Length == 0)
                    continue;

                foreach (var impactable in impactables)
                {
                    NPCController controller = impactable.Controller as NPCController;
                    if (controller != null && controller.NPCDataController.NPCData.IsDead)
                    {
                        OnDeadBodyDetected?.Invoke(impactable.Controller);
                        return;
                    }
                }
            }
        }
    }
}