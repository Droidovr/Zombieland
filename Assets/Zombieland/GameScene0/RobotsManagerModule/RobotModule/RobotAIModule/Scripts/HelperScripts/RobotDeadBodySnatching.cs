using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAIModule
{
    public class RobotDeadBodySnatching : MonoBehaviour
    {
        private const float CHECK_INTERVAL = 0.5f;

        private IRobotAIController _robotAIController;
        private NavMeshAgent _navMeshAgent;
        private Transform _targetTransform;
        private bool _isMovingToTarget = false;


        public void Init(IRobotAIController robotAIController)
        {
            _robotAIController = robotAIController;
            _navMeshAgent = _robotAIController.RobotController.RobotVisualBodyController.RobotInScene.GetComponent<NavMeshAgent>();
        }

        public void Snatching(Transform leftFoot)
        {
            _targetTransform = leftFoot;
            _navMeshAgent.SetDestination(leftFoot.position);
            _isMovingToTarget = true;

            InvokeRepeating(nameof(CheckReachedTarget), 0f, CHECK_INTERVAL);
        }

        private void CheckReachedTarget()
        {
            if (_isMovingToTarget && !_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    _isMovingToTarget = false;
                    CancelInvoke(nameof(CheckReachedTarget));
                    OnReachedTarget();
                }
            }
        }

        private void OnReachedTarget()
        {
            Debug.Log("Цель достигнута!");
            PerformAction();
        }

        private void PerformAction()
        {
            Debug.Log("Выполняем действие после достижения цели");
            // Здесь опишите действия, которые нужно выполнить после достижения цели
        }

        private void OnDisable()
        {
            // Убедимся, что Invoke отменен при отключении объекта
            CancelInvoke(nameof(CheckReachedTarget));
        }
    }
}