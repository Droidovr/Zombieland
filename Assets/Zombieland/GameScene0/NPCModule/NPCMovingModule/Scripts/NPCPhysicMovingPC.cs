using System;
using UnityEngine;
using UnityEngine.AI;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public class NPCPhysicMovingPC : MonoBehaviour, INPCPhysicMoving
    {
        public event Action<float, bool> OnMoving;

        private const float GRAVITY = 9.8f;
        private const float ROTATION_SMOOTH_TIME = 0.03f;
        private const float MIN_VECTORMOVE_MAGITUDE = 0.1f;

        private float _verticalSpeed;
        private UnityEngine.CharacterController _unityCharacterController;
        private NavMeshAgent _navMeshAgent;
        private INPCMovingController _nPCMovingController;
        public bool _isActive;
        private Vector2 _velocity;
        private Vector2 _smoothDeltaPosition;
        private float _smoothTime = 0.1f;


        public void Init(INPCMovingController nPCMovingController)
        {
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _nPCMovingController = nPCMovingController;
            _nPCMovingController.NPCController.NPCAnimationController.OnAnimationMove += OnAnimatorMoveHandler;
            _nPCMovingController.NPCController.NPCSpawnController.OnSpawn += SpawnHandler;
        }

        public void ActivateMoving(bool isActive)
        {
            _unityCharacterController.enabled = isActive;
            _isActive = isActive;
        }

        public void Disable()
        {
            _nPCMovingController.NPCController.NPCAnimationController.OnAnimationMove -= OnAnimatorMoveHandler;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            CalculateGravity();
            SynchronizeAnimatorAndNavMeshAgent();
        }

        private void SynchronizeAnimatorAndNavMeshAgent()
        {
            Vector3 worldDeltaPosition = _navMeshAgent.nextPosition - transform.position;
            worldDeltaPosition.y = 0f;

            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            float smooth = Mathf.Min(1, Time.deltaTime / _smoothTime);
            _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

            _velocity = _smoothDeltaPosition / Time.deltaTime;
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _velocity = Vector2.Lerp(Vector2.zero, _velocity, _navMeshAgent.remainingDistance / _navMeshAgent.stoppingDistance);
            }

            bool shouldMove = _velocity.magnitude > 0.5f
                && _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance;

            OnMoving.Invoke(_velocity.magnitude, shouldMove);

            float deltaMagnitude = worldDeltaPosition.magnitude;
            if (deltaMagnitude > _navMeshAgent.radius / 2f)
            {
                _unityCharacterController.Move(_velocity * Time.deltaTime);
            }
        }

        private void SpawnHandler(Vector3 vector, Quaternion quaternion)
        {
            System.Numerics.Vector3 patrolPoint = _nPCMovingController.NPCController.NPCDataController.NPCData.NPCSpawnData.PatrolPoint;
            _navMeshAgent.SetDestination(new Vector3(patrolPoint.X, patrolPoint.Y, patrolPoint.Z));
            Debug.Log("NavMesh Destination: " + _navMeshAgent.destination);
        }

        private void OnAnimatorMoveHandler(Vector3 animatorRootPosition)
        {
            if (_unityCharacterController.enabled)
            {
                Vector3 rootPosition = animatorRootPosition;
                rootPosition.y = _navMeshAgent.nextPosition.y;
                transform.position = rootPosition;
                _navMeshAgent.nextPosition = rootPosition;
            }
        }

        private void CalculateGravity()
        {
            if (_unityCharacterController.enabled)
            {
                _verticalSpeed -= _unityCharacterController.isGrounded ? _verticalSpeed : GRAVITY * Time.deltaTime;
                _unityCharacterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
            }
        }
    }
}