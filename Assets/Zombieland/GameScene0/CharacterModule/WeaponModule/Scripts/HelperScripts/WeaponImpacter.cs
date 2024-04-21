using UnityEngine;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponImpacter
    {
        private IWeaponController _weaponController;
        private Transform _weaponPointFire;

        public WeaponImpacter(IWeaponController weaponController) 
        { 
            _weaponController = weaponController;
            _weaponPointFire = _weaponController.CharacterController.VisualBodyController.WeaponInScene.GetComponent<Transform>().Find("PointFire");
        }

        public Impact GetCurrentImpact()
        {
            Impact impact = _weaponController.CharacterController.RootController.GameDataController.GetData<Impact>(_weaponController.CharacterController.EquipmentController.CurrentImpactID);
            
            impact.ImpactData.ImpactOwner = _weaponController.CharacterController;

            impact.ImpactData.FollowTargetTransform = _weaponController.CharacterController.AimingController.GetTarget();

            impact.ImpactData.ObjectSpawnPosition = _weaponPointFire.position;

            impact.ImpactData.ObjectRotation = AddShotSpread(impact.ImpactData.FollowTargetTransform);

            return impact;
        }

        private Quaternion AddShotSpread(Transform target)
        {
            Quaternion finalRotation = new Quaternion();

            float shotAccuracy = _weaponController.Weapon.WeaponData.ShotAccuracy;
            float deviationAngle = Random.Range(-shotAccuracy, shotAccuracy);

            if (_weaponController.Weapon.WeaponData.HasTarget)
            {
                Quaternion deviationRotation = Quaternion.Euler(0f, 0f, deviationAngle);

                Vector3 startPosition = _weaponPointFire.position;

                Vector3 directionToTarget = (target.position - startPosition).normalized;

                Quaternion directionQuaternion = Quaternion.LookRotation(directionToTarget);

                finalRotation = deviationRotation * directionQuaternion;
            }
            else
            {
                finalRotation = _weaponPointFire.rotation * Quaternion.Euler(0f, 0f, deviationAngle);
            }

            return finalRotation;
        }
    }
}