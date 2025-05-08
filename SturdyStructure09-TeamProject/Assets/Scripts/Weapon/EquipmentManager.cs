using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public void Equip(Item item)
{
    
    var oldWeapon = player.GetComponentInChildren<WeaponHandler>();
    if (oldWeapon != null)
        Destroy(oldWeapon.gameObject);

    GameObject newWeapon = Instantiate(item.weaponPrefab, player.transform.Find("WeaponPivot"));


    player.SetWeaponHandler(newWeapon.GetComponent<WeaponHandler>());

   
    equippedItem = item;
    if (weaponIconUI != null)
        weaponIconUI.sprite = item.icon;
}