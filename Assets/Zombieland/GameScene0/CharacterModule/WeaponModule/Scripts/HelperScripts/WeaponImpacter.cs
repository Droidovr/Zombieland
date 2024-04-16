using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponImpacter
    {
        private IWeaponController _weaponController;

        public WeaponImpacter(IWeaponController weaponController) 
        { 
            _weaponController = weaponController;
        }

        public Impact GetCurrentImpact()
        {
            Impact impact = _weaponController.CharacterController.RootController.GameDataController.GetData<Impact>(_weaponController.CurrentImpactID);
            
            impact.ImpactData.ImpactOwner = _weaponController.CharacterController;

            impact.ImpactData.FollowTargetTransform = _weaponController.CharacterController.AimingController.GetTarget();

            impact.ImpactData.ObjectSpawnPosition = _weaponController.CharacterController.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(_weaponController.CharacterController.VisualBodyController.WeaponPointFire.position);

            impact.ImpactData.ObjectRotation = AddShotSpread(impact.ImpactData.FollowTargetTransform);

            return impact;
        }

        private Quaternion AddShotSpread(Transform target)
        {
            Quaternion finalRotation = new Quaternion();

            if (_weaponController.Weapon.WeaponData.HasTarget)
            {
                float shotAccuracy = _weaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);
                Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);

                Vector3 startPosition = _weaponController.CharacterController.VisualBodyController.CharacterInScene.GetComponent<Transform>().TransformPoint(_weaponController.CharacterController.VisualBodyController.WeaponPointFire.position);

                Vector3 directionToTarget = (target.position - startPosition).normalized;

                Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                finalRotation = deviationRotation * directionQuaternion;
            }
            else
            {
                float shotAccuracy = _weaponController.Weapon.WeaponData.ShotAccuracy;
                float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);
                finalRotation = _weaponController.CharacterController.VisualBodyController.WeaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
            }

            return finalRotation;
        }
    }
}