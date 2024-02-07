using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterRagdoll : MonoBehaviour
    {
        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private Transform _hipsTransform;
        private List<RagdollBone> _ragdollBones = new List<RagdollBone>();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _hipsTransform = _animator.GetBoneTransform(HumanBodyBones.Hips);
            _ragdollBones.Add(new RagdollBone(_hipsTransform));

            Transform[] allChildren = GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                CharacterJoint characterJoint = child.GetComponent<CharacterJoint>();
                if (characterJoint != null)
                {
                    RagdollBone ragdollBone = new RagdollBone(child);
                    _ragdollBones.Add(ragdollBone);
                }
            }
        }

        void Update()
        {

        }

        public void Hit(Vector3 forceDirection, Vector3 hitPosition)
        {
            foreach (RagdollBone ragdollBone in _ragdollBones)
            {
                Collider boneCollider = ragdollBone.BoneCollider;

                if (boneCollider.bounds.Contains(hitPosition))
                {
                    ActivateRagdoll();

                    ragdollBone.BoneRigidbody.AddForceAtPosition(forceDirection, hitPosition, ForceMode.Impulse);

                    Invoke("DeactivateRagdoll", 0.2f);

                    break;
                }
            }
        }

        public void ActivateRagdoll()
        {
            _unityCharacterController.enabled = false;
            _animator.enabled = false;

            foreach (RagdollBone ragdollBone in _ragdollBones)
            {
                ragdollBone.BoneRigidbody.isKinematic = false;
            }
        }

        public void DeactivateRagdoll()
        {
            foreach (RagdollBone ragdollBone in _ragdollBones)
            {
                ragdollBone.BoneRigidbody.isKinematic = true;
            }

            _animator.enabled = true;
            _unityCharacterController.enabled = true;
        }
    }
}