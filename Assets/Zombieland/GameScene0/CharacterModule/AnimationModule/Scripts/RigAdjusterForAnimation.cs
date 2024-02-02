using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class RigAdjusterForAnimation
    {
        private const float TIME_TO_SHIFT_BONES_TO_START_ANIMATION = 0.5f;

        private GameObject _gameObject;
        private AnimationClip _clip;

        private List<Transform> _bonesStart = new List<Transform>();
        private List<Transform> _bonesFinish = new List<Transform>();



        private BoneTransformData[] _bonesBeaforeAnimation;
        private BoneTransformData[] _bonesAtStartAnimation;


        public RigAdjusterForAnimation(GameObject gameObject, Animator animator, string clipName)
        {
            Debug.Log("<color=red>Constructor RigAdjusterForAnimation</color>");
            
            _gameObject = gameObject;
            _clip = GetAnimationClip(animator, clipName);

            GetCurrentBonesInStateRagdoll(gameObject);
            GetBonesStartFrameAnimation(_clip);

            foreach (var item in _bonesStart)
            {
                Debug.Log($"<color=blue>Start: {item.name}, {item.position}</color>");
            }

            foreach (var item in _bonesFinish)
            {
                Debug.Log($"<color=green>Start: {item.name}, {item.position}</color>");
            }
        }

        private AnimationClip GetAnimationClip(Animator animator, string clipName)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip;
                }
            }

            return null;
        }

        private void GetCurrentBonesInStateRagdoll(GameObject gameObject)
        {
            Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (child != gameObject.transform && child.GetComponent<SkinnedMeshRenderer>() == null)
                {
                    _bonesStart.Add(child);
                }
            }
        }

        private void GetBonesStartFrameAnimation(AnimationClip clip)
        { 
            Vector3 initPosition = _gameObject.transform.position;
            Quaternion initRotation = _gameObject.transform.rotation;

            float frameTimeAnimationClip = 0;
            _clip.SampleAnimation(_gameObject, frameTimeAnimationClip);

            for (int i = 0; i < _bonesStart.Count; i++)
            {
                _bonesFinish.Add(_bonesStart[i]);
            }

            _gameObject.transform.position = initPosition;
            _gameObject.transform.rotation = initRotation;
        }
    }
}