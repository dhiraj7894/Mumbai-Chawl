using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Control;

namespace MumbaiChawls.Player
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

            foreach (WeaponHolderSlot weaponHolderSlot in weaponHolderSlots)
            {
                if(weaponHolderSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponHolderSlot;
                }
                else if(weaponHolderSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponHolderSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(CombatItem combatItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(combatItem);
            }else if (!isLeft)
            {
                rightHandSlot.LoadWeaponModel(combatItem);
            }
        }

    }
}
