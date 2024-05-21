using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public class VisualSensor : MonoBehaviour
    {
        public event Action<IController, bool> OnVisualDetect;

        [SerializeField] private Light _visualSensorLight;

        private INPCVisualController _nPCVisualController;
        private float _viewAngle = 60f; // ”гол обзора
        private float _range = 10f;
        private Color _originalLightColor;

        private Collider _detectedCharacter; // —сылка на обнаруженного персонажа

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
            _visualSensorLight.spotAngle = _viewAngle;
            _visualSensorLight.innerSpotAngle = _viewAngle;
            _visualSensorLight.range = _range;
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
