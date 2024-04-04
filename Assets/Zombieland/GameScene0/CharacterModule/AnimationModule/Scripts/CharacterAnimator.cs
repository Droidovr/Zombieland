using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterAnimator : MonoBehaviour
    {
        public event Action<Vector3> OnAnimatorMoveHandler;

        private const string PC_ANIMATOR = "PCAnimatorController";
        private const string MOBILE_ANIMATOR = "Character0MobileAnimator";
        private const float DAMP_TIME = 0.05f;

        private ICharacterController _characterController;
        private Animator _animator;

        private void Update()
        {
            _animator.SetFloat("RealMovingSpeed", _characterController.CharacterMovingController.RealMovingSpeed, DAMP_TIME, Time.deltaTime);

            Vector3 forwardAnimation = GetForwardAnimation();

            _animator.SetFloat("DirectionX", forwardAnimation.x, DAMP_TIME, Time.deltaTime);
            _animator.SetFloat("DirectionZ", forwardAnimation.z, DAMP_TIME, Time.deltaTime);
        }

        public void Init(ICharacterController CharacterController)
        {
            _animator = GetComponent<Animator>();

#if UNITY_STANDALONE || UNITY_EDITOR
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(PC_ANIMATOR);
#else
            _animator.runtimeAnimatorController = Resources.Load<AnimatorController>(MOBILE_ANIMATOR);
#endif

            _characterController = CharacterController;
        }

        private void OnAnimatorMove()
        {
            if (_animator.enabled)
            {
                OnAnimatorMoveHandler?.Invoke(_animator.deltaPosition);
            }
        }

        private Vector3 GetForwardAnimation()
        {
            Vector3 forwardAnimation = Vector3.zero; // Используем Vector3.zero для инициализации вектора

            float angle = Vector3.Angle(transform.forward, Vector3.forward);


            if (_characterController.CharacterMovingController.DirectionWalk.z == 1 && _characterController.CharacterMovingController.DirectionWalk.x == 0)
            {
                if (angle < 45f)
                {
                    Debug.Log("Объект смотрит вверх");
                    forwardAnimation.z = 1f;
                }
                else if (angle > 135f)
                {
                    Debug.Log("Объект смотрит вниз");
                    forwardAnimation.z = -1f;
                }

                if (transform.forward.x > 0)
                {
                    Debug.Log("Объект смотрит вправо");
                    forwardAnimation.x = -1f;
                }
                else
                {
                    Debug.Log("Объект смотрит влево");
                    forwardAnimation.x = 1f;
                }
            }

            if (_characterController.CharacterMovingController.DirectionWalk.z == -1 && _characterController.CharacterMovingController.DirectionWalk.x == 0)
            {
                if (angle < 45f)
                {
                    Debug.Log("Объект смотрит вверх");
                    forwardAnimation.z = -1f;
                }
                else if (angle > 135f)
                {
                    Debug.Log("Объект смотрит вниз");
                    forwardAnimation.z = 1f;
                }

                if (transform.forward.x > 0)
                {
                    Debug.Log("Объект смотрит вправо");
                    forwardAnimation.x = 1f;
                }
                else
                {
                    Debug.Log("Объект смотрит влево");
                    forwardAnimation.x = -1f;
                }
            }

            //if (_characterController.CharacterMovingController.DirectionWalk.z == 0 && _characterController.CharacterMovingController.DirectionWalk.x == 1)
            //{

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = 1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = -1f;
            //    }
            //}

            //if (_characterController.CharacterMovingController.DirectionWalk.z == 0 && _characterController.CharacterMovingController.DirectionWalk.x == -1)
            //{

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = -1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = 1f;
            //    }
            //}

            //if (_characterController.CharacterMovingController.DirectionWalk.z == 1 && _characterController.CharacterMovingController.DirectionWalk.x == 1)
            //{
            //    if (angle < 45f)
            //    {
            //        Debug.Log("Объект смотрит вверх");
            //        forwardAnimation.z = 1f;
            //    }
            //    else if (angle > 135f)
            //    {
            //        Debug.Log("Объект смотрит вниз");
            //        forwardAnimation.z = -1f;
            //    }

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = 1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = -1f;
            //    }
            //}

            //if (_characterController.CharacterMovingController.DirectionWalk.z == 1 && _characterController.CharacterMovingController.DirectionWalk.x == -1)
            //{
            //    if (angle < 45f)
            //    {
            //        Debug.Log("Объект смотрит вверх");
            //        forwardAnimation.z = 1f;
            //    }
            //    else if (angle > 135f)
            //    {
            //        Debug.Log("Объект смотрит вниз");
            //        forwardAnimation.z = -1f;
            //    }

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = -1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = 1f;
            //    }
            //}

            //if (_characterController.CharacterMovingController.DirectionWalk.z == -1 && _characterController.CharacterMovingController.DirectionWalk.x == -1)
            //{
            //    if (angle < 45f)
            //    {
            //        Debug.Log("Объект смотрит вверх");
            //        forwardAnimation.z = -1f;
            //    }
            //    else if (angle > 135f)
            //    {
            //        Debug.Log("Объект смотрит вниз");
            //        forwardAnimation.z = 1f;
            //    }

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = -1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = 1f;
            //    }
            //}

            //if (_characterController.CharacterMovingController.DirectionWalk.z == -1 && _characterController.CharacterMovingController.DirectionWalk.x == 1)
            //{
            //    if (angle < 45f)
            //    {
            //        Debug.Log("Объект смотрит вверх");
            //        forwardAnimation.z = -1f;
            //    }
            //    else if (angle > 135f)
            //    {
            //        Debug.Log("Объект смотрит вниз");
            //        forwardAnimation.z = 1f;
            //    }

            //    if (transform.forward.x > 0)
            //    {
            //        Debug.Log("Объект смотрит вправо");
            //        forwardAnimation.x = 1f;
            //    }
            //    else
            //    {
            //        Debug.Log("Объект смотрит влево");
            //        forwardAnimation.x = -1f;
            //    }
            //}



            Debug.Log("forwardAnimation" + forwardAnimation);
            return forwardAnimation;
        }


    }
}
