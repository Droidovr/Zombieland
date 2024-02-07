using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterRagdoll : MonoBehaviour
    {
        private const string STAND_UP_FRONT = "StandUpFront";
        private const string STAND_UP_BACK = "StandUpBack";

        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private Transform _hipsTransform;
        private List<RagdollBone> _ragdollBones = new List<RagdollBone>();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _hipsTransform = _animator.GetBoneTransform(HumanBodyBones.Hips);
            Transform[] allChildren = GetComponentsInChildren<Transform>();

            _ragdollBones.Add(new RagdollBone(_hipsTransform));
            ExtractRagdollBones(allChildren, _ragdollBones);
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

                    float boneMass = ragdollBone.BoneRigidbody.mass;
                    float deactivationTime = Mathf.Clamp(boneMass * 0.05f, 0.01f, 0.1f);

                    //Invoke("DeactivateRagdoll", deactivationTime);

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

            TranslateTransformToHips();

            StartCoroutine(TransitionToAnimation());
        }

        private IEnumerator TransitionToAnimation()
        {
            float smoothTime = 2f;
            float elapsedTime = 0f;

            List<RagdollBone> animationBones = GetRagdollBonesFromAnimation(STAND_UP_FRONT);

            while (elapsedTime < smoothTime)
            {
                for (int i = 0; i < _ragdollBones.Count; i++)
                {
                    Transform ragdollBonesTransform = _ragdollBones[i].BoneTransform;
                    Transform animationBonesTransform = animationBones[i].BoneTransform;

                    ragdollBonesTransform.localPosition = Vector3.Lerp(ragdollBonesTransform.localPosition, animationBonesTransform.localPosition, elapsedTime / smoothTime);
                    ragdollBonesTransform.localRotation = Quaternion.Slerp(ragdollBonesTransform.localRotation, animationBonesTransform.localRotation, elapsedTime / smoothTime);
                }

                elapsedTime += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            _animator.Play(STAND_UP_FRONT);
            _animator.enabled = true;
            _unityCharacterController.enabled = true;
        }

        private List<RagdollBone> GetRagdollBonesFromAnimation(string nameAnimation)
        {
            GameObject tempObject = gameObject;

            tempObject.transform.position = transform.position;
            tempObject.transform.rotation = transform.rotation;

            Animator tempAnimator = tempObject.AddComponent<Animator>();
            tempAnimator.runtimeAnimatorController = _animator.runtimeAnimatorController;
            tempAnimator.avatar = _animator.avatar;
            tempAnimator.Play(nameAnimation, 0, 0);

            List<RagdollBone> animationBones = new List<RagdollBone>();
            Transform animationHips = tempAnimator.GetBoneTransform(HumanBodyBones.Hips);
            animationBones.Add(new RagdollBone(animationHips));
            Transform[] transforms = tempAnimator.gameObject.GetComponentsInChildren<Transform>();
            ExtractRagdollBones(transforms, animationBones);

            //Destroy(tempObject);

            return animationBones;
        }

        private void ExtractRagdollBones(Transform[] transforms, List<RagdollBone> ragdollBones)
        {
            foreach (Transform bone in transforms)
            {
                CharacterJoint characterJoint = bone.GetComponent<CharacterJoint>();
                if (characterJoint != null)
                {
                    RagdollBone ragdollBone = new RagdollBone(bone);
                    ragdollBones.Add(ragdollBone);
                }
            }
        }

        private void TranslateTransformToHips()
        {
            Vector3 hipsPosition = _hipsTransform.position;

            transform.position = hipsPosition;

            _hipsTransform.position = hipsPosition;
        }
    }
}