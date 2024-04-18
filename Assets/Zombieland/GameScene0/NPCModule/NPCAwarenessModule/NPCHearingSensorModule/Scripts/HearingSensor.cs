using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule
{
    public class HearingSensor : MonoBehaviour
    {
        [SerializeField] 
        private float _hearingRange = 5f;
        [SerializeField] 
        private Color _hearingRangeColour = new Color(0.3f, 0.8f, 1f, 0.1f);

        public float HearingRange => _hearingRange;
        public Color HearingRangeColour => _hearingRangeColour;
        
        private float _sqrHearingCircleRange;

        private Transform _targetTransform;
        private Action<bool> _onCharacterInsideZone;
        
        private float _currentCheckZoneTime;
        private const float CHECK_ZONE_TIME = 0.1f;
        
        private bool _isTargetInsideZone;

        private void Start()
        {
            _sqrHearingCircleRange = _hearingRange * _hearingRange;
        }
        
        private void Update()
        {
            _currentCheckZoneTime += Time.deltaTime;
            if(_currentCheckZoneTime < CHECK_ZONE_TIME) return;
            CheckInZoneTargets();
            _currentCheckZoneTime = 0f;
        }
        
        public void Init(Transform targetTransform, Action<bool> onTargetInsideZone)
        {
            _targetTransform = targetTransform;
            _onCharacterInsideZone = onTargetInsideZone;
        }

        private void CheckInZoneTargets()
        {
            var distanceVector = transform.position - _targetTransform.position;
           
            // check Distance
            if (distanceVector.sqrMagnitude > _sqrHearingCircleRange)
            {
                RegisterTarget();
                return;
            }
            DeregisterTarget();
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