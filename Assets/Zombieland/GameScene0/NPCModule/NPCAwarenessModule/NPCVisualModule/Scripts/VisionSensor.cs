using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public class VisionSensor : MonoBehaviour
    {
        public event Action<IController, bool> OnVisualDetect;

        private INPCVisionController _nPCVisualController;
        private Light _visualSensorLight;

        private float _viewAngle = 60f; // ”гол обзора
        private float _range = 10f;
        private float _intensity = 50f;
        private bool isVisible;

        private Collider _detectedCharacter; // —сылка на обнаруженного персонажа

        public void Init(INPCVisionController nPCVisualController)
        {
            _nPCVisualController = nPCVisualController;

            _visualSensorLight = nPCVisualController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.Find("VisualSensorLight").GetComponent<Light>();
            _visualSensorLight.spotAngle = _viewAngle;
            _visualSensorLight.range = _range;
            _visualSensorLight.intensity = 0f;

            _nPCVisualController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController.OnStealth += CharacterStealthHandler;
        }

        public void Destroy()
        {
            _nPCVisualController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController.OnStealth -= CharacterStealthHandler;
        }

        private void CharacterStealthHandler(bool isStealth)
        {
            _visualSensorLight.intensity = isStealth ? _intensity : 0f;
        }

        private void Update()
        {
            RaycastHit hit;
            Vector3 rayDirection = transform.forward;

            if (Physics.Raycast(transform.position, rayDirection, out hit, _visualSensorLight.range))
            {
                if (Vector3.Angle(rayDirection, hit.transform.position - transform.position) < _viewAngle / 2)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
                    {
                        if (_detectedCharacter == null)
                        {
                            Impactable impactable = hit.collider.gameObject.GetComponent<Impactable>();
                            if (impactable != null)
                            {
                                IController controller = impactable.Controller;
                                OnVisualDetect?.Invoke(controller, true);
                                Debug.Log("Character detected: " + hit.collider.gameObject.name);
                                _detectedCharacter = hit.collider;
                            }
                        }
                    }
                }
            }
            else
            {
                if (_detectedCharacter != null)
                {
                    Impactable impactable = _detectedCharacter.gameObject.GetComponent<Impactable>();
                    if (impactable != null)
                    {
                        IController controller = impactable.Controller;
                        OnVisualDetect?.Invoke(controller, false);
                        Debug.Log("Character exited detection zone: " + _detectedCharacter.gameObject.name);
                        _detectedCharacter = null;
                    }
                }
            }
        }
    }
}
