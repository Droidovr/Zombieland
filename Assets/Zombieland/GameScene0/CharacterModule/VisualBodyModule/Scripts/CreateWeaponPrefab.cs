using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class CreateWeaponPrefab
    {


        public GameObject CtreateWeapon(string weaponPrefabName, Transform characterInScene)
        {
            GameObject prefab = Resources.Load<GameObject>(weaponPrefabName);

            Transform weaponPoint = GameObject.Find("WeaponPoint").GetComponent<Transform>();

            return GameObject.Instantiate(prefab, weaponPoint);
        }

        public void Destroy(GameObject weaponInScene)
        {
            GameObject.Destroy(weaponInScene);
        }
    }
}