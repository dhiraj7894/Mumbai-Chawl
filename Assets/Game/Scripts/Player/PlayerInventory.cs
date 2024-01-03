using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Control;

namespace MumbaiChawls.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public CombatItem rightWeapon;
        public CombatItem leftWeapon;

        public CombatItem unarmedCombatItem;


        public CombatItem[] weaponInLeftCombatItem = new CombatItem[1];
        public CombatItem[] weaponInRightCombatItem = new CombatItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<CombatItem> combatInventory;

        private void Awake()
        {
            weaponSlotManager = GetComponent<WeaponSlotManager>();
           
        }

        private void Start()
        {
            rightWeapon = unarmedCombatItem;
            leftWeapon = unarmedCombatItem;

            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

        }


        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
            if (currentRightWeaponIndex == 0 && weaponInRightCombatItem[0] != null)
            {
                rightWeapon = weaponInRightCombatItem[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightCombatItem[currentRightWeaponIndex], false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            if(currentRightWeaponIndex > weaponInRightCombatItem.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedCombatItem;
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            } 
        }

        public void ChangeLeftWeapon()
        {
            currentRightWeaponIndex = currentRightWeaponIndex - 1;
            if (currentRightWeaponIndex == -1 && unarmedCombatItem != null)
            {
                rightWeapon = unarmedCombatItem;
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex - 1;
            }

            if (currentRightWeaponIndex < - 1)
            {
                currentRightWeaponIndex = -1;


            }
        }
    }
}
