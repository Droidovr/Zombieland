using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateWeaponPrefab
    {


        public GameObject CtreateWeapon(Weapon weapon, Transform characterInScene)
        {
            GameObject prefab = Resources.Load<GameObject>(weapon.WeaponData.PrefabName);

            Transform weaponPoint = GameObject.Find("WeaponPoint").GetComponent<Transform>();

            return GameObject.Instantiate(prefab, weaponPoint);
        }

        public void DestroyWeapon(GameObject weaponInScene)
        {
            GameObject.Destroy(weaponInScene);
        }
    }
}