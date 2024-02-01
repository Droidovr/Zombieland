using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class Ragdoll
    {
        private const string STAND_UP_BACK_NAME_ANIMATION = "StandUpBack";
        private const string STAND_UP_FRONT_NAME_ANIMATION = "StandUpFont";
        private const int DEFAULT_LAYER_ANIMATOR = -1;

        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private List<Rigidbody> _rigidbodies;
        private Transform _parent;
        private Transform _hipsBone;

        private RigAdjusterForAnimation _rigAdjusterForBackStandingUpAnimation;
        private RigAdjusterForAnimation _rigAdjusterForFrontStandingUpAnimation;

        public Ragdoll(GameObject gameObject)
        {
            _animator = gameObject.GetComponent<Animator>();

            _unityCharacterController = gameObject.GetComponent<UnityEngine.CharacterController>();

            _rigidbodies = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());

            _parent = gameObject.transform;

            _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);

            RagdollHandler(true);
        }

        public async void Hit(Vector3 force, Vector3 hitPosition)
        {
            RagdollHandler(false);

            Rigidbody injuredRigidbody = _rigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPosition)).First();
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);

            //await Task.Delay(300);

            //RagdollHandler(true);
        }

        public void StandUp()
        {
            RagdollHandler(true);

            AdjustParentPositionToHipsBone();
            AdjustParentRorationToHipsBone();


            if (IsFrontUp())
            {
                _animator.Play(STAND_UP_BACK_NAME_ANIMATION, DEFAULT_LAYER_ANIMATOR, 0f);
            }
            else
            {
                _animator.Play(STAND_UP_FRONT_NAME_ANIMATION, DEFAULT_LAYER_ANIMATOR, 0f);
            }
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

        private void AdjustParentPositionToHipsBone()
        {
            Vector3 initHipsPosition = _hipsBone.position;
            _parent.position = initHipsPosition;

            RaycastHit hit;
            if (Physics.Raycast(_parent.position, Vector3.down, out hit, Mathf.Infinity))
            {
                _parent.position = new Vector3(_parent.position.x, hit.point.y, _parent.position.z);
            }

            _hipsBone.position = initHipsPosition;
        }

        private void AdjustParentRorationToHipsBone()
        {
            Vector3 initHipsPosition = _hipsBone.position;
            Quaternion initHipsRotation = _hipsBone.rotation;

            Vector3 directionForRotate = _hipsBone.up;

            if (IsFrontUp())
            {
                directionForRotate *= -1;
            }

            directionForRotate.y = 0;

            Quaternion correctionRotation = Quaternion.FromToRotation(_parent.forward, directionForRotate.normalized);
            _parent.rotation *= correctionRotation;

            _hipsBone.position = initHipsPosition;
            _hipsBone.rotation = initHipsRotation;
        }

        private bool IsFrontUp()
        {
            return Vector3.Dot(_hipsBone.up, Vector3.up) > 0;
        }
    }
}