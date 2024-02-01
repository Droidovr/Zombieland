using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class RigAdjusterForAnimation
    {
        private const float TIME_TO_SHIFT_BONES_TO_START_ANIMATION = 0.5f;

        private AnimationClip _clip;
        private MonoBehaviour _view;

        private List<Transform> _bones;
        private BoneTransformData[] _bonesBeaforeAnimation;
        private BoneTransformData[] _bonesAtStartAnimation;

        private Coroutine _shiftBonesToStandingUpAnimation;

        public RigAdjusterForAnimation(AnimationClip clip, IEnumerable<Transform> bones, MonoBehaviour view)
        {
            _clip = clip;
            _view = view;
            _bones = new List<Transform>(bones);

            _bonesBeaforeAnimation = new BoneTransformData[_bones.Count];
            _bonesAtStartAnimation = new BoneTransformData[_bones.Count];

            for (int i = 0; i < _bones.Count; i++)
            {
                _bonesBeaforeAnimation[i] = new BoneTransformData();
                _bonesAtStartAnimation[i] = new BoneTransformData();
            }

            SaveBonesDataFromStartAnimation();
        }

        public void Adjust(Action callback)
        {
            SaveCurrentBonesDataTo(_bonesBeaforeAnimation);

            if (_shiftBonesToStandingUpAnimation != null)
            { 
                _view.StopCoroutine(_shiftBonesToStandingUpAnimation );
            }

            _shiftBonesToStandingUpAnimation = _view.StartCoroutine(ShiftBonesToAnimation(callback));
        }

        private IEnumerator ShiftBonesToAnimation(Action callback)
        {
            float progress = 0;

            while (progress < TIME_TO_SHIFT_BONES_TO_START_ANIMATION)
            { 
                progress += Time.deltaTime;
                float progressInPercantage = progress / TIME_TO_SHIFT_BONES_TO_START_ANIMATION;

                for (int i = 0; i < _bones.Count; i++)
                {
                    _bones[i].localPosition = Vector3.Lerp(_bonesBeaforeAnimation[i].Position, _bonesAtStartAnimation[i].Position, progressInPercantage);
                    _bones[i].localRotation = Quaternion.Lerp(_bonesBeaforeAnimation[i].Rotation, _bonesAtStartAnimation[i].Rotation, progressInPercantage);
                }

                yield return null;
            }

            callback.Invoke();
        }

        private void SaveCurrentBonesDataTo(BoneTransformData[] bones)
        {
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].Position = _bones[i].localPosition;
                bones[i].Rotation = _bones[i].localRotation;
            }
        }

        private void SaveBonesDataFromStartAnimation()
        {
            Vector3 initPosition = _view.transform.position;
            Quaternion initQuaternion = _view.transform.rotation;

            _clip.SampleAnimation(_view.gameObject, 0);
            SaveCurrentBonesDataTo(_bonesAtStartAnimation);

            _view.transform.position = initPosition;
            _view.transform.rotation = initQuaternion;
        }
    }
}