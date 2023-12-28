using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Core;


namespace MumbaiChawls.Enemy
{

    public class EnemyAnimManager : CharacterAnimManager
    {
        EnemyManager enemyManager;
        private void Start()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponent<EnemyManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRB.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRB.velocity = velocity;
        }
    }
}
