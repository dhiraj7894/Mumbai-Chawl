using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Control;

namespace MumbaiChawls.Player
{

    public class PlayerAttacker : MonoBehaviour
    {
        AnimHandler animHandler;

        private void Awake()
        {
            animHandler = GetComponent<AnimHandler>();

        }

        public void HandleLightAttack(CombatItem combatItem)
        {
            animHandler.PlayTargetAnimation(combatItem.Lead_Jab_01, true);
        }
        public void HandleHeavyAttack(CombatItem combatItem)
        {
            animHandler.PlayTargetAnimation(combatItem.Hook_01, true);
        }

    }
}