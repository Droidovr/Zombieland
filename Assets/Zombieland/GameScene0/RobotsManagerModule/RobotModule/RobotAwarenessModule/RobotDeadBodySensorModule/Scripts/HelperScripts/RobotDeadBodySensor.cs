using System;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule
{
    public class RobotDeadBodySensor : MonoBehaviour
    {
        public event Action<IController> OnDeadBodyDetected;

        private const float DETECTION_RANGE = 10.0f;
        private const float CHECK_INTERVAL = 1.0f;


        public void Init()
        {
            InvokeRepeating(nameof(DetectDeadBody), 0, CHECK_INTERVAL);
        }

        private void DetectDeadBody()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, DETECTION_RANGE))
                return;

            Impactable impactable = hit.collider.GetComponentInChildren<Impactable>();
            if (impactable == null)
                return;

            NPCController controller = impactable.Controller as NPCController;
            Debug.Log("Detected object with controller: " + controller.NPCDataController.NPCData.Name);
            
            if (controller == null || !controller.NPCDataController.NPCData.IsDead)
                return;

            OnDeadBodyDetected?.Invoke(impactable.Controller);
            Debug.Log("Detected object with controller: " + controller.NPCDataController.NPCData.Name);
        }
    }
}