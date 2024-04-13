using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.ImpactModule;
using static UnityEngine.Rendering.HDROutputUtils;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponResurser : MonoBehaviour
    {
        public  bool IsReserveResurce = false;

        private IWeaponController _weaponController;

        public WeaponResurser(IWeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        public void ResourceOperation(bool isOperation,List<ConsumableResource> consumableResources)
        {
            
            if (!IsReserveResurce && !isOperation) 
            {
                return;
            }

            IsReserveResurce = isOperation;

            _weaponController.CharacterController.EquipmentController.CurrentImpactCount += isOperation ? -1 : 1;

            for (int i = 0; i < consumableResources.Count; i++)
            {
                switch (consumableResources[i].ResourceType)
                {
                    case ResourceType.Stamina:
                        if (isOperation)
                        {
                            _weaponController.CharacterController.CharacterDataController.CharacterData.Stamina -= consumableResources[i].Value;
                        }
                        else
                        {
                            _weaponController.CharacterController.CharacterDataController.CharacterData.Stamina += consumableResources[i].Value;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}