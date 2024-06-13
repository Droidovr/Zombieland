using System;
using UnityEngine;
using UnityEngine.AI;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAnimationModule
{
    public class RobotAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveEvent;
        public event Action<bool> OnAnimationAttack;

        private IRobotAnimationController _robotAnimationController;
        private Animator _animator;
        private bool _isWeaponAnimation = false;
        private Weapon _weapon;


        public void Init(IRobotAnimationController robotAnimationController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(robotAnimationController.RobotController.RobotDataController.RobotData.NameAnimatorControllerPC);
#else
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(robotAnimationController.RobotController.RobotDataController.RobotData.NameAnimatorControllerMobile);
#endif

            _robotAnimationController = robotAnimationController;
            _robotAnimationController.RobotController.RobotMovingController.OnMoving += MovingHandler;
            // _nPCAnimatorController.NPCController.NPCAIController.OnFire += AIFireHandler;

            // Test
            Vector3 patrolPoint = new Vector3
                (
                    _robotAnimationController.RobotController.RobotSpawnData.PatrolPoint.X,
                    _robotAnimationController.RobotController.RobotSpawnData.PatrolPoint.Y,
                    _robotAnimationController.RobotController.RobotSpawnData.PatrolPoint.Z
                );
            GetComponent<NavMeshAgent>().SetDestination(patrolPoint);
        }

        public void Disable()
        {
            _robotAnimationController.RobotController.RobotMovingController.OnMoving -= MovingHandler;
            // _nPCAnimatorController.NPCController.NPCAIController.OnFire -= AIFireHandler;
        }


        private void MovingHandler(float speed, bool isMove)
        {
            _animator.SetBool("IsMove", isMove);
        }

        private void AttackHandler()
        {
            OnAnimationAttack?.Invoke(true);
        }

        private void AIFireHandler(bool isFire)
        {
            if (_robotAnimationController.RobotController.RobotVisualBodyController.WeaponInScene != null)
            {
                _animator.SetBool("IsAttack", isFire);
            }
        }


        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimatorMoveEvent?.Invoke(_animator.rootPosition);
            }
        }
    }
}