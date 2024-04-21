using Zombieland.GameScene0.ImpactModule;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class FirePermiser
    {
        private IWeaponController _weaponController;

        public FirePermiser(IWeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        public bool CheckFirePermission(Impact impact)
        {
            // Test
            return true;

            bool isCheckResource = ResourcesConsumption(impact);
            bool isDead = _weaponController.CharacterController.CharacterDataController.CharacterData.IsDead;
            bool isStunned = _weaponController.CharacterController.CharacterDataController.CharacterData.IsStunned;

            if (isCheckResource && isDead && isStunned)
            {
                if (_weaponController.Weapon.WeaponData.HasTarget)
                {
                    if (_weaponController.CharacterController.AimingController.GetTarget() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ResourcesConsumption(Impact impact)
        {
            bool isCheckResource = _weaponController.CharacterController.EquipmentController.CurrentImpactCount >= 0;

            for (int i = 0; i < impact.ImpactData.ConsumableResources.Count; i++)
            {
                switch (impact.ImpactData.ConsumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (impact.ImpactData.ConsumableResources[i].Value >= _weaponController.CharacterController.CharacterDataController.CharacterData.Stamina)
                        {
                            isCheckResource = true;
                        }
                        else
                        {
                            isCheckResource = false;
                        }
                        break;

                    default:
                        isCheckResource = false;
                        break;
                }
            }

            return isCheckResource;
        }
    }
}