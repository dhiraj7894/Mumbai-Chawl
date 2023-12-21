using MumbaiChawls.Control.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy.Stats
{

    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;


        private Animator anim;
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }
        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth /*- damage*/;
            if (currentHealth > 0) anim.Play(AnimHash.TAKEDAMAGE);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                anim.Play(AnimHash.DYING);
            }
        }
    }
}