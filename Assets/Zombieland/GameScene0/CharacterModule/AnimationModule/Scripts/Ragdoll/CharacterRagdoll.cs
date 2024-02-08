using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class CharacterRagdoll : MonoBehaviour
    {
        private const string STAND_UP_FRONT = "StandUpFront";
        private const string STAND_UP_BACK = "StandUpBack";
        private const float RAGDOLL_TO_MECANIM_BLEND_TIME = 1f;

        private readonly List<RagdollComponent> _ragdollComponents = new List<RagdollComponent>();

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
                    _ragdollComponents.Add(new RagdollComponent(boneTransform));
                }
            }

            ActivateRagdollParts(false);
        }

        void LateUpdate()
        {
            if (_ragdollState == RagdollState.BlendToAnimation)
            {
                float ragdollBlendAmount = 1f - Mathf.InverseLerp(_ragdollingEndTime, _ragdollingEndTime + RAGDOLL_TO_MECANIM_BLEND_TIME, Time.time);

                foreach (RagdollComponent ragdollComponent in _ragdollComponents)
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
            foreach (RagdollComponent component in _ragdollComponents)
            {
                Collider collider = component.Collider;

                if (collider != null)
                {
                    if (collider.bounds.Contains(hitPosition))
                    {
                        ActivateRagdollParts(true);
                        _ragdollState = RagdollState.Ragdolled;

                        component.RigidBody.AddForceAtPosition(forceDirection, hitPosition, ForceMode.Impulse);

                        break;
                    }
                }
            }
        }

        public void GetUp()
        {
            if (_ragdollState != RagdollState.Ragdolled)
            {
                return;
            }

            _ragdollingEndTime = Time.time;
            _ragdollState = RagdollState.BlendToAnimation;

            Vector3 shiftPos = _hipsTransform.position - transform.position;
            shiftPos.y = GetDistanceToFloor(shiftPos.y);

            MoveNodeWithoutChildren(shiftPos);

            foreach (RagdollComponent ragdollComponent in _ragdollComponents)
            {
                ragdollComponent.StoredRotation = ragdollComponent.Transform.localRotation;
                ragdollComponent.PrivRotation = ragdollComponent.Transform.localRotation;

                ragdollComponent.StoredPosition = ragdollComponent.Transform.localPosition;
                ragdollComponent.PrivPosition = ragdollComponent.Transform.localPosition;
            }

            ActivateRagdollParts(false);
            string getUpAnimation = CheckIfLieOnBack() ? STAND_UP_FRONT : STAND_UP_BACK;
            _animator.Play(getUpAnimation, 0, 0);
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
            Vector3 hipsPosition = _hipsTransform.position;

            transform.position = hipsPosition;

            _hipsTransform.position = hipsPosition;


            //StartCoroutine(SmoothMoveNode(shiftPos, 0.5f));

            //Vector3 ragdollDirection = GetRagdollDirection();

            //_hipsTransform.position -= shiftPos;
            //transform.position += shiftPos;

            //Vector3 dirProection = Vector3.ProjectOnPlane(ragdollDirection, Vector3.up);
            //transform.eulerAngles = dirProection;
            ////if (CheckIfLieOnBack())
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180f, transform.eulerAngles.z);

            ////Debug.LogError("Stop 1");


            //transform.rotation = Quaternion.FromToRotation(transform.forward, ragdollDirection) * transform.rotation;
            //_hipsTransform.rotation = Quaternion.FromToRotation(ragdollDirection, transform.forward) * _hipsTransform.rotation;
        }

        private IEnumerator SmoothMoveNode(Vector3 shiftPos, float duration)
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;

            Vector3 endPosition = startPosition + shiftPos;
            Quaternion endRotation = Quaternion.FromToRotation(transform.forward, GetRagdollDirection()) * startRotation;

            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

                yield return null;
                elapsedTime += Time.deltaTime;
            }

            // Убедитесь, что объект достигает конечной позиции и поворота точно
            transform.position = endPosition;
            transform.rotation = endRotation;
        }

        private Vector3 GetRagdollDirection()
        {
            Vector3 ragdolledFeetPosition = _animator.GetBoneTransform(HumanBodyBones.Hips).position;
            Vector3 ragdolledHeadPosition = _animator.GetBoneTransform(HumanBodyBones.Head).position;
            Vector3 ragdollDirection = ragdolledFeetPosition - ragdolledHeadPosition;
            ragdollDirection.y = 0;
            ragdollDirection = ragdollDirection.normalized;

            if (CheckIfLieOnBack())
            {
                return ragdollDirection;
            }
            else
            {
                return -ragdollDirection;
            }
        }

        private bool CheckIfLieOnBack()
        {
            Vector3 leftUpperLegPosition = _animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position;
            Vector3 rightUpperLegPosition = _animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).position;
            Vector3 hipsPosition = _hipsTransform.position;

            leftUpperLegPosition -= hipsPosition;
            leftUpperLegPosition.y = 0f;
            rightUpperLegPosition -= hipsPosition;
            rightUpperLegPosition.y = 0f;

            Quaternion rotationFromLeftToRight = Quaternion.FromToRotation(leftUpperLegPosition, Vector3.right);
            Vector3 relativePositionToHips = rotationFromLeftToRight * rightUpperLegPosition;

            return relativePositionToHips.z < 0f;
        }

        private void ActivateRagdollParts(bool activate)
        {
            _unityCharacterController.enabled = !activate;
            _animator.enabled = !activate;

            foreach (RagdollComponent ragdollComponet in _ragdollComponents)
            {
                ragdollComponet.IsKinematikBone(!activate);

                if (activate)
                {
                    StartCoroutine(FixTransformAndEnableJoint(ragdollComponet.Joint));
                }
            }
        }

        private IEnumerator FixTransformAndEnableJoint(CharacterJoint joint)
        {
            if (joint == null || !joint.autoConfigureConnectedAnchor)
            {
                yield break;
            }

            SoftJointLimit highTwistLimit = joint.highTwistLimit;
            SoftJointLimit lowTwistLimit = joint.lowTwistLimit;
            SoftJointLimit swing1Limit = joint.swing1Limit;
            SoftJointLimit swing2Limit = joint.swing2Limit;

            SoftJointLimit curHighTwistLimit = highTwistLimit;
            SoftJointLimit curLowTwistLimit = lowTwistLimit;
            SoftJointLimit curSwing1Limit = swing1Limit;
            SoftJointLimit curSwing2Limit = swing2Limit;

            Vector3 startConnectedPosition = joint.connectedBody.transform.InverseTransformVector(joint.transform.position - joint.connectedBody.transform.position);

            float jointParameterChangeDurationTime = 0.3f;
            float timeElapsedStart = 0f;
            float timeElapsedEnd = 1f;
            float maxRotationLimit = 177f;

            joint.autoConfigureConnectedAnchor = false;
            for (float timeElapsed = timeElapsedStart; timeElapsed < timeElapsedEnd; timeElapsed += Time.deltaTime / jointParameterChangeDurationTime)
            {
                Vector3 newConPosition = Vector3.Lerp(startConnectedPosition, joint.connectedAnchor, timeElapsed);
                joint.connectedAnchor = newConPosition;

                curHighTwistLimit.limit = Mathf.Lerp(maxRotationLimit, highTwistLimit.limit, timeElapsed);
                curLowTwistLimit.limit = Mathf.Lerp(-maxRotationLimit, lowTwistLimit.limit, timeElapsed);
                curSwing1Limit.limit = Mathf.Lerp(maxRotationLimit, swing1Limit.limit, timeElapsed);
                curSwing2Limit.limit = Mathf.Lerp(maxRotationLimit, swing2Limit.limit, timeElapsed);

                joint.highTwistLimit = curHighTwistLimit;
                joint.lowTwistLimit = curLowTwistLimit;
                joint.swing1Limit = curSwing1Limit;
                joint.swing2Limit = curSwing2Limit;


                yield return null;
            }
            joint.connectedAnchor = joint.connectedAnchor;
            yield return new WaitForFixedUpdate();
            joint.autoConfigureConnectedAnchor = true;


            joint.highTwistLimit = highTwistLimit;
            joint.lowTwistLimit = lowTwistLimit;
            joint.swing1Limit = swing1Limit;
            joint.swing2Limit = swing2Limit;
        }
    }
}