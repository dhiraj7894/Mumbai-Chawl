using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MumbaiChawls.Control
{
    [CreateAssetMenu(menuName ="Items/Combat Item")]
    public class CombatItem : Item
    {
        public Transform prefab;
        public bool isUnarmed;

        [Header("Animations Name")]

        public string Lead_Jab_01;
        public string Hook_01;
    }
}
