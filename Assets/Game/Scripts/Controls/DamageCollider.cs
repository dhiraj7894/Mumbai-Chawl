using MumbaiChawls.Player.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MumbaiChawls.Control
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentWeaponDamage = 25;
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);            
            damageCollider.enabled = false;
            damageCollider.isTrigger = true;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisbaleDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ColliderTags.PLAYER))
            {
                PlayerStats stats = other.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage(currentWeaponDamage);
                }
            }
            if (other.CompareTag(ColliderTags.ENEMY))
            {
                EnemyStats stats = other.GetComponent<EnemyStats>();
                if (stats != null)
                {
                    stats.TakeDamage(currentWeaponDamage);
                }
            }

        }
    }
}
