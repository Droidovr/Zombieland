using UnityEngine;
using UnityEngine.TextCore.Text;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateWeaponPrefab
    {
        public GameObject CtreateWeapon(Weapon weapon, IVisualBodyController visualBodyController)
        {
            //GameObject prefab = Resources.Load<GameObject>(weapon.WeaponData.PrefabName);
            GameObject prefab = Resources.Load<GameObject>("Weapons/Wreanch");

            Transform weaponPoint = visualBodyController.CharacterInScene.GetComponent<Transform>().Find("WeaponPoint");

            return GameObject.Instantiate(prefab, weaponPoint.position, Quaternion.identity);
        }
    }
}