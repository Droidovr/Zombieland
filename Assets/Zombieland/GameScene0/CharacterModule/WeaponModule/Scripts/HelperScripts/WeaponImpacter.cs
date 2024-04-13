using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponImpacter
    {
        private ICharacterController _characterController;

        public WeaponImpacter(ICharacterController characterController) 
        { 
            _characterController = characterController;
        }

        public Impact GetCurrentImpact()
        {
            Impact impact = _characterController.RootController.GameDataController.GetData<Impact>(_characterController.WeaponController.CurrentImpactName);
            
            impact.ImpactData.ImpactOwner = _characterController;

            impact.ImpactData.FollowTargetTransform = _characterController.AimingController.GetTarget();

            impact.ImpactData.ObjectSpawnPosition = _characterController.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(_characterController.VisualBodyController.WeaponPointFire.position);

            impact.ImpactData.ObjectRotation = AddShotSpread(impact.ImpactData.FollowTargetTransform);

            return impact;
        }

        private Quaternion AddShotSpread(Transform target)
        {
            Quaternion finalRotation = new Quaternion();

            if (_characterController.WeaponController.Weapon.WeaponData.HasTarget)
            {
                float shotAccuracy = _characterController.WeaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);
                Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);

                Vector3 startPosition = _characterController.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(_characterController.VisualBodyController.WeaponPointFire.position);

                Vector3 directionToTarget = (target.position - startPosition).normalized;

                Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                finalRotation = deviationRotation * directionQuaternion;
            }
            else
            {
                float shotAccuracy = _characterController.WeaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);
                finalRotation = _characterController.VisualBodyController.WeaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
            }

            return finalRotation;
        }
    }
}