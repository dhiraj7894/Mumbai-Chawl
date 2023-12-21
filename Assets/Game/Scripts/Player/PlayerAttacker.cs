using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Control;

namespace MumbaiChawls.Player
{

    public class PlayerAttacker : MonoBehaviour
    {
        AnimHandler animHandler;
        InputHandler inputHandler;


        public string lastAttack;

        private void Awake()
        {
            animHandler = GetComponent<AnimHandler>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleLightAttack(CombatItem combatItem)
        {
            animHandler.PlayTargetAnimation(combatItem.Lead_Jab_01, true);
            lastAttack = combatItem.Lead_Jab_01;
        }
        public void HandleHeavyAttack(CombatItem combatItem)
        {
            animHandler.PlayTargetAnimation(combatItem.Hook_01, true);
            lastAttack = combatItem.Hook_01;
        }

        public void HandleWeaponCombo(CombatItem combatItem)
        {
            if (inputHandler.comboFlag)
            {
                animHandler.anim.SetBool(AnimHash.CANDOCOMBO, true);
                if (lastAttack == combatItem.Lead_Jab_01)
                {
                    animHandler.PlayTargetAnimation(combatItem.Lead_Jab_02, true);
                }

            }

            
        }

    }
}