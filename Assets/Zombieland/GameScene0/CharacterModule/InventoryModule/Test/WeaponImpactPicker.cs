using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombieland;
using Zombieland.GameScene0.CharacterModule.InventoryModule;

public class WeaponImpactPicker : MonoBehaviour
{
    private IInventoryController _inventoryController;
    public void Init(IController inventoryController)
    {
        _inventoryController = inventoryController as IInventoryController;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) { _inventoryController.EquipWeaponIntoActiveSlot("Pistol_0", 0); }
       
    }
}
