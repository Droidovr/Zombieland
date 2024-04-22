using System;
using UnityEngine;
using UnityEngine.AI;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshHandler : MonoBehaviour
    {
        public bool IsMoving => !_agent.isStopped;
        
        private NavMeshAgent _agent;
        private Transform _targetTransform;
        private bool _isTargetUpdatable;

        private float _currentPathUpdateTime;
        private const float PATH_UPDATE_TIME = 0.1f;

        private Action _targetReached;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    
        void Update()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _targetReached?.Invoke();
                return;
            }
            
            if(!_isTargetUpdatable) return;
            _currentPathUpdateTime += Time.deltaTime;
            if(_currentPathUpdateTime < PATH_UPDATE_TIME) return;
            _agent.SetDestination(_targetTransform.position);
            _currentPathUpdateTime = 0f;
        }

        public void Init(float Speed)
        {
            _agent.speed = Speed;
        }

        public void MoveToTarget(Vector3 targetPosition, float stoppingDistance, Action onTargetReached)
        {
            _agent.stoppingDistance = stoppingDistance;
            _agent.SetDestination(targetPosition);
            _targetReached = onTargetReached;
            _isTargetUpdatable = false;
        }

        public void FollowTarget(Transform targetTransform, float stoppingDistance, Action onTargetReached)
        {
            _agent.stoppingDistance = stoppingDistance;
            _agent.SetDestination(targetTransform.position);
            _targetReached = onTargetReached;
            _targetTransform = targetTransform;
            _isTargetUpdatable = true;
        }

        public void StopMoving()
        {
            _agent.ResetPath();
            _targetReached = null;
            _isTargetUpdatable = false;
        }
    }
}
