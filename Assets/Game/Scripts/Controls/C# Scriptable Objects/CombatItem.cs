using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
namespace MumbaiChawls.Control
{
    [CreateAssetMenu(menuName ="Items/Combat Item")]
    public class CombatItem : Item
    {
        public Transform prefab;
        public bool isUnarmed;

        [Header("Animations Name")]
        public AnimatorController AnimController;
        public string Lead_Jab_01;
        public string Lead_Jab_02;
        public string Hook_01;
    }
}
