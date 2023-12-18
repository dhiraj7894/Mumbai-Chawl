using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls;
using MumbaiChawls.Control.UI;

namespace MumbaiChawls.Player.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        public HealthBar healthBar;
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;


        private AnimHandler animHandler;

        private void Awake()
        {
            animHandler = GetComponent<AnimHandler>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10; 
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);
            if (currentHealth > 0) animHandler.PlayTargetAnimation(AnimHash.TAKEDAMAGE, true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animHandler.PlayTargetAnimation(AnimHash.DYING, true);
            }
        }
    }
}
