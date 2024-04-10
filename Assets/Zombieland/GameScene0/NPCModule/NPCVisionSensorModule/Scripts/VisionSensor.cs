using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SensorModule.ImpactableSensorModule;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class VisionSensor : MonoBehaviour
    {
        [SerializeField] 
        private float _visionConeAngle = 60f;
        [SerializeField] 
        private float _visionConeRange = 5f;
        [SerializeField] 
        private Color _visionConeColour = new Color(0.9f, 0f, 0f, 0.4f);
        [SerializeField] 
        private LayerMask _detectionMask;
        [SerializeField] 
        private Transform _eyesTransform;

        public float VisionConeAngle => _visionConeAngle;
        public float VisionConeRange => _visionConeRange;
        public Color VisionConeColour => _visionConeColour;

        private Transform _targetTransform;
        private Action<bool> _onCharacterInsideZone;
        
        private float _sqrVisionConeRange;
        private float _cosHalfVisionConeAngle;
        
        private float _currentCheckZoneTime;
        private const float CHECK_ZONE_TIME = 0.1f;

        private bool _isTargetInsideZone;

        private void Start()
        {
            _sqrVisionConeRange = _visionConeRange * _visionConeRange;
            _cosHalfVisionConeAngle = Mathf.Cos(_cosHalfVisionConeAngle * 0.5f * Mathf.Deg2Rad);
        }

        private void Update()
        {
            _currentCheckZoneTime += Time.deltaTime;
            if(_currentCheckZoneTime < CHECK_ZONE_TIME) return;
            CheckInZoneTargets();
            _currentCheckZoneTime = 0f;
        }

        public void Init(Transform targetTransform, Action<bool> onCharacterInsideZone)
        {
            _targetTransform = targetTransform;
            _onCharacterInsideZone = onCharacterInsideZone;
        }

        private void CheckInZoneTargets()
        {
            var distanceVector = transform.position - _targetTransform.position;
            
            // check Distance
            if (distanceVector.sqrMagnitude > _sqrVisionConeRange)
            {
                DeregisterTarget();
                return;
            }
            
            // check VisionCone
            if(Vector3.Dot(distanceVector.normalized, transform.forward) < _cosHalfVisionConeAngle)
            {
                DeregisterTarget();
                return;
            }
            
            // raycast
            if (!Physics.Raycast(_eyesTransform.position, _eyesTransform.forward, out var raycastHit, _visionConeRange,
                _detectionMask)) 
            {
                DeregisterTarget();
                return;
            }
            
            // check component
            if (raycastHit.collider.TryGetComponent<IImpactable>(out var impactable))
            {
                // Check if impactable is CharacterImpactable
                RegisterTarget();
            }
        }

        private void RegisterTarget()
        {
            if(_isTargetInsideZone)
                return;

            _isTargetInsideZone = true;
            _onCharacterInsideZone?.Invoke(true);
        }

        private void DeregisterTarget()
        {
            if(!_isTargetInsideZone)
                return;

            _isTargetInsideZone = false;
            _onCharacterInsideZone?.Invoke(false);
        }
    }
}