using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterRagdoll : MonoBehaviour
    {
        private const string STAND_UP_FRONT = "StandUpFront";
        private const string STAND_UP_BACK = "StandUpBack";
        private const float RAGDOLL_TO_MECANIM_BLEND_TIME = 0.5f;

        private readonly List<RagdollComponent> _ragdollComponets = new List<RagdollComponent>();

        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private Transform _hipsTransform;
        private RagdollState _ragdollState = RagdollState.Animated;
        private float _ragdollingEndTime;


        void Start()
        {
            _animator = GetComponent<Animator>();
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _hipsTransform = _animator.GetBoneTransform(HumanBodyBones.Hips);

            for (int i = 0; i < (int)HumanBodyBones.LastBone; i++)
            {
                Transform boneTransform = _animator.GetBoneTransform((HumanBodyBones)i);
                if (boneTransform != null)
                {
                    _ragdollComponets.Add(new RagdollComponent(boneTransform));
                }
            }

            ActivateRagdollParts(false);
        }

        void LateUpdate()
        {
            if (_ragdollState == RagdollState.BlendToAnimation)
            {
                float ragdollBlendAmount = 1f - Mathf.InverseLerp(_ragdollingEndTime, _ragdollingEndTime + RAGDOLL_TO_MECANIM_BLEND_TIME, Time.time);

                foreach (RagdollComponent ragdollComponent in _ragdollComponets)
                {
                    if (ragdollComponent.PrivRotation != ragdollComponent.Transform.localRotation)
                    {
                        ragdollComponent.PrivRotation = Quaternion.Slerp(ragdollComponent.Transform.localRotation, ragdollComponent.StoredRotation, ragdollBlendAmount);
                        ragdollComponent.Transform.localRotation = ragdollComponent.PrivRotation;
                    }

                    if (ragdollComponent.PrivPosition != ragdollComponent.Transform.localPosition)
                    {
                        ragdollComponent.PrivPosition = Vector3.Slerp(ragdollComponent.Transform.localPosition, ragdollComponent.StoredPosition, ragdollBlendAmount);
                        ragdollComponent.Transform.localPosition = ragdollComponent.PrivPosition;
                    }
                }

                if (Mathf.Abs(ragdollBlendAmount) < Mathf.Epsilon)
                {
                    _ragdollState = RagdollState.Animated;
                }
            }
        }

        public void Hit(Vector3 forceDirection, Vector3 hitPosition)
        {
            ActivateRagdollParts(true);
            _ragdollState = RagdollState.Ragdolled;

            //foreach (var ragdollComponent in _ragdollComponets)
            //{
            //    if (ragdollComponent.RigidBody != null && ragdollComponent.Collider != null)
            //    {
            //        // Проверяем, содержит ли коллайдер точку hitPosition
            //        if (ragdollComponent.Collider.bounds.Contains(hitPosition))
            //        {
            //            Debug.Log("<color=red>" + ragdollComponent.Transform.name + "</color>");
            //            ragdollComponent.RigidBody.AddForceAtPosition(forceDirection, hitPosition, ForceMode.Impulse);
            //            break; // Прерываем цикл после обнаружения первого соответствующего коллайдера
            //        }
            //    }
            //}

            RagdollComponent injuredRagdollComponent = _ragdollComponets
                                                            .Where(ragdollComponent => ragdollComponent.RigidBody != null)
                                                            .OrderBy(ragdollComponent => Vector3.Distance(ragdollComponent.RigidBody.position, hitPosition))
                                                            .FirstOrDefault();

            //Debug.Log("<color=red>" + injuredRagdollComponent.Transform.name + "</color>");

            //if (injuredRagdollComponent != null)
            //{
            //    injuredRagdollComponent.RigidBody.AddForceAtPosition(forceDirection, hitPosition, ForceMode.Impulse);
            //}
        }

        public void GetUp()
        {
            _ragdollingEndTime = Time.time;
            _ragdollState = RagdollState.BlendToAnimation;

            Vector3 shiftPos = _hipsTransform.position - transform.position;
            shiftPos.y = GetDistanceToFloor(shiftPos.y);

            MoveNodeWithoutChildren(shiftPos);

            foreach (RagdollComponent ragdollComponent in _ragdollComponets)
            {
                ragdollComponent.StoredRotation = ragdollComponent.Transform.localRotation;
                ragdollComponent.PrivRotation = ragdollComponent.Transform.localRotation;

                ragdollComponent.StoredPosition = ragdollComponent.Transform.localPosition;
                ragdollComponent.PrivPosition = ragdollComponent.Transform.localPosition;
            }

            string getUpAnimation = CheckIfLieOnBack() ? STAND_UP_FRONT : STAND_UP_BACK;
            _animator.Play(getUpAnimation, 0, 0);
            ActivateRagdollParts(false);
        }

        private float GetDistanceToFloor(float currentY)
        {
            RaycastHit[] hits = Physics.RaycastAll(new Ray(_hipsTransform.position, Vector3.down));
            float distFromFloor = float.MinValue;

            foreach (RaycastHit hit in hits)
                if (!hit.transform.IsChildOf(transform))
                    distFromFloor = Mathf.Max(distFromFloor, hit.point.y);

            if (Mathf.Abs(distFromFloor - float.MinValue) > Mathf.Epsilon)
                currentY = distFromFloor - transform.position.y;

            return currentY;
        }

        private void MoveNodeWithoutChildren(Vector3 shiftPos)
        {
            Vector3 ragdollDirection = GetRagdollDirection();

            _hipsTransform.position -= shiftPos;
            transform.position += shiftPos;

            transform.rotation = Quaternion.FromToRotation(transform.forward, ragdollDirection) * transform.rotation;
            _hipsTransform.rotation = Quaternion.FromToRotation(ragdollDirection, transform.forward) * _hipsTransform.rotation;
        }

        private Vector3 GetRagdollDirection()
        {
            Vector3 ragdolledFeetPosition = _animator.GetBoneTransform(HumanBodyBones.Hips).position;
            Vector3 ragdolledHeadPosition = _animator.GetBoneTransform(HumanBodyBones.Head).position;
            Vector3 ragdollDirection = ragdolledFeetPosition - ragdolledHeadPosition;
            ragdollDirection.y = 0;
            ragdollDirection = ragdollDirection.normalized;

            if (CheckIfLieOnBack())
                return ragdollDirection;
            else
                return -ragdollDirection;
        }

        private bool CheckIfLieOnBack()
        {
            var left = _animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position;
            var right = _animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).position;
            var hipsPos = _hipsTransform.position;

            left -= hipsPos;
            left.y = 0f;
            right -= hipsPos;
            right.y = 0f;

            var q = Quaternion.FromToRotation(left, Vector3.right);
            var t = q * right;

            return t.z < 0f;
        }

        private void ActivateRagdollParts(bool activate)
        {
            _unityCharacterController.enabled = !activate;
            _animator.enabled = !activate;

            foreach (var ragdollComponet in _ragdollComponets)
            {
                ragdollComponet.IsKinematikBone(!activate);

                if (activate)
                {
                    StartCoroutine(FixTransformAndEnableJoint(ragdollComponet));
                }
            }
        }

        private IEnumerator FixTransformAndEnableJoint(RagdollComponent joint)
        {
            if (joint.Joint == null || !joint.Joint.autoConfigureConnectedAnchor)
            {
                yield break;
            }

            SoftJointLimit highTwistLimit = joint.Joint.highTwistLimit;
            SoftJointLimit lowTwistLimit = joint.Joint.lowTwistLimit;
            SoftJointLimit swing1Limit = joint.Joint.swing1Limit;
            SoftJointLimit swing2Limit = joint.Joint.swing2Limit;

            SoftJointLimit curHighTwistLimit = highTwistLimit;
            SoftJointLimit curLowTwistLimit = lowTwistLimit;
            SoftJointLimit curSwing1Limit = swing1Limit;
            SoftJointLimit curSwing2Limit = swing2Limit;

            float aTime = 0.3f;
            Vector3 startConnectedPosition = joint.Joint.connectedBody.transform.InverseTransformVector(joint.Joint.transform.position - joint.Joint.connectedBody.transform.position);

            joint.Joint.autoConfigureConnectedAnchor = false;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Vector3 newConPosition = Vector3.Lerp(startConnectedPosition, joint.ConnectedAnchorDefault, t);
                joint.Joint.connectedAnchor = newConPosition;

                curHighTwistLimit.limit = Mathf.Lerp(177, highTwistLimit.limit, t);
                curLowTwistLimit.limit = Mathf.Lerp(-177, lowTwistLimit.limit, t);
                curSwing1Limit.limit = Mathf.Lerp(177, swing1Limit.limit, t);
                curSwing2Limit.limit = Mathf.Lerp(177, swing2Limit.limit, t);

                joint.Joint.highTwistLimit = curHighTwistLimit;
                joint.Joint.lowTwistLimit = curLowTwistLimit;
                joint.Joint.swing1Limit = curSwing1Limit;
                joint.Joint.swing2Limit = curSwing2Limit;


                yield return null;
            }
            joint.Joint.connectedAnchor = joint.ConnectedAnchorDefault;
            yield return new WaitForFixedUpdate();
            joint.Joint.autoConfigureConnectedAnchor = true;


            joint.Joint.highTwistLimit = highTwistLimit;
            joint.Joint.lowTwistLimit = lowTwistLimit;
            joint.Joint.swing1Limit = swing1Limit;
            joint.Joint.swing2Limit = swing2Limit;
        }
    }
}