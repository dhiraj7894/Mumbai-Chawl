using MumbaiChawls.Player.Stats;
using UnityEngine;

namespace MumbaiChawls.Control
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damageAmount;
        private void OnTriggerEnter(Collider other)
        {
            
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats)
            {
                stats.TakeDamage(damageAmount);
            }
        }
    }
}