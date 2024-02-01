using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class NewBehaviourScript : MonoBehaviour
    {
        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private List<Rigidbody> _rigidbodies;
        private Transform _parent;
        private Transform _hipsBone;

        [SerializeField] private List<Transform> _bonesToMove;
        [SerializeField] private Transform _standUpStartPosition;
        [SerializeField] private float _transitionTime = 1f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _parent = transform;
            _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

            // «апуск регдолла при инициализации
            RagdollHandler(true);
        }

        public async void Hit(Vector3 force, Vector3 hitPosition)
        {
            // ќтключение регдолла перед нанесением удара
            RagdollHandler(false);

            // ѕоиск наиближайшей кости и нанесение удара
            Rigidbody injuredRigidbody = _rigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPosition)).First();
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);

            // ќжидание перед включением регдолла
            //await Task.Delay(300);

            // ¬ключение регдолла после нанесени€ удара
            //RagdollHandler(true);
        }

        public void StandUp()
        {
            // ѕлавное перемещение костей в позицию начала анимации "Stand Up" и запуск анимации
            SmoothTransitionToStandUpAnimation(_transitionTime);
        }

        private void RagdollHandler(bool isDisabled)
        {
            _unityCharacterController.enabled = isDisabled;
            _animator.enabled = isDisabled;

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = isDisabled;
            }
        }

        private void SmoothTransitionToStandUpAnimation(float transitionTime)
        {
            StartCoroutine(TransitionCoroutine(transitionTime));
        }

        private IEnumerator TransitionCoroutine(float transitionTime)
        {
            // «апоминаем начальные позиции и повороты костей
            Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();
            Dictionary<Transform, Quaternion> initialRotations = new Dictionary<Transform, Quaternion>();

            foreach (Transform bone in _bonesToMove)
            {
                initialPositions[bone] = bone.position;
                initialRotations[bone] = bone.rotation;
            }

            // ¬ычисл€ем конечные позиции и повороты костей дл€ плавного перемещени€
            Vector3 targetPosition = _standUpStartPosition.position;
            Quaternion targetRotation = _standUpStartPosition.rotation;

            float elapsedTime = 0f;
            while (elapsedTime < transitionTime)
            {
                // ѕлавное изменение позиций и поворотов костей
                foreach (Transform bone in _bonesToMove)
                {
                    bone.position = Vector3.Lerp(initialPositions[bone], targetPosition, elapsedTime / transitionTime);
                    bone.rotation = Quaternion.Slerp(initialRotations[bone], targetRotation, elapsedTime / transitionTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ”становка конечных позиций и поворотов костей
            foreach (Transform bone in _bonesToMove)
            {
                bone.position = targetPosition;
                bone.rotation = targetRotation;
            }

            // «апуск анимации "Stand Up" после завершени€ плавного перемещени€
            _animator.Play("Stand Up");
        }
    }
}
