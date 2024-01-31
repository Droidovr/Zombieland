using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class Ragdoll
    {
        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private List<Rigidbody> _rigidbodies;
        private Transform _parent;
        private Transform _hipsBone;

        public Ragdoll(GameObject gameObject)
        {
            _animator = gameObject.GetComponent<Animator>();

            _unityCharacterController = gameObject.GetComponent<UnityEngine.CharacterController>();

            _rigidbodies = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());

            _parent = gameObject.transform;
            
            _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);

            Debug.Log("_hipsBone: " + _hipsBone.position);

            Disable();
        }

        public async void Hit(Vector3 force, Vector3 hitPosition)
        {
            Enable();

            Rigidbody injuredRigidbody = _rigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPosition)).First();
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);

            //await Task.Delay(300);

            //Disable();
        }

        public void StandUp()
        {
            Disable();

            AdjustParentRorationToHipsBone();
            AdjustParentPositionToHipsBone();


            if (IsFrontUp())
            {
                _animator.Play("Stand Up", -1, 0f);
            }
            else
            {
                _animator.Play("Standing Up", -1, 0f);
            }
        }

        private void Enable()
        {
            _animator.enabled = false;
            _unityCharacterController.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        private void Disable()
        {
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }

            _unityCharacterController.enabled = true;
            _animator.enabled = true;
        }

        private void AdjustParentPositionToHipsBone()
        { 
            Vector3 initHipsPosition = _hipsBone.position;
            _parent.position = initHipsPosition;

            if (Physics.Raycast(_parent.position, Vector3.down, out RaycastHit hit, 5, 1 << LayerMask.NameToLayer("Ground")))
            {
                _parent.position = new Vector3(_parent.position.x, hit.point.y, _parent.position.z);
            }

            _hipsBone.position = initHipsPosition;
        }

        private void AdjustParentRorationToHipsBone()
        {
            Vector3 initHipsPosition = _hipsBone.position;
            Quaternion initHipsRotation = _hipsBone.rotation;

            Vector3 directionForRotate = -_hipsBone.up;

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