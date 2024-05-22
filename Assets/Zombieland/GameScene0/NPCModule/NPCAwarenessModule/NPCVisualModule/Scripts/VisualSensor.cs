using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public class VisualSensor : MonoBehaviour
    {
        public event Action<IController, bool> OnVisualDetect;

        [SerializeField] private Light _visualSensorLight;

        private float VIEW_ANGLE = 60f;
        private float RANGE = 10f;
        private const float VISIBILITY_DETECTION_TIMEOUT = 2f;
        private const float REPEATE_DETECT_TIME = 0.1f;

        private INPCVisualController _nPCVisualController;
        private Color _originalLightColor;
        private Collider _detectedCharacter;
        private IController _cashController;

        public void Init(INPCVisualController nPCVisualController)
        {
            _nPCVisualController = nPCVisualController;
            _nPCVisualController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController.OnStealth += CharacterStealthHandler;
        }

        public void Destroy()
        {
            _nPCVisualController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController.OnStealth -= CharacterStealthHandler;
        }

        private void Start()
        {
            _visualSensorLight.spotAngle = VIEW_ANGLE;
            _visualSensorLight.innerSpotAngle = VIEW_ANGLE;
            _visualSensorLight.range = RANGE;
            _originalLightColor = _visualSensorLight.color;
            _visualSensorLight.color = Color.black;
        }

        private void CharacterStealthHandler(bool isStealth)
        {
            _visualSensorLight.color = isStealth ? _originalLightColor : Color.black;
            InvokeRepeating("VisualDetect", 0f, 0.1f);
        }

        private void VisualDetect()
        {
            RaycastHit hit;
            Vector3 rayDirection = transform.forward;

            if (Physics.Raycast(transform.position, rayDirection, out hit, _visualSensorLight.range))
            {
                if (Vector3.Angle(rayDirection, hit.transform.position - transform.position) < VIEW_ANGLE / 2)
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
                if (_detectedCharacter != null && _cashController == null)
                {
                    Impactable impactable = _detectedCharacter.gameObject.GetComponent<Impactable>();
                    if (impactable != null)
                    {
                        IController controller = impactable.Controller;
                        _cashController = controller;
                        Invoke(nameof(ExitZonaDetect), VISIBILITY_DETECTION_TIMEOUT);
                        Debug.Log("Character exited detection zone: " + _detectedCharacter.gameObject.name);
                        _detectedCharacter = null;
                    }
                }
            }
        }

        private void ExitZonaDetect()
        {
            OnVisualDetect?.Invoke(_cashController, false);
            _cashController = null;
        }
    }
}
