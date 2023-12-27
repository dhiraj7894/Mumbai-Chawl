using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Core;


namespace MumbaiChawls.Enemy
{

    public class EnemyAnimManager : CharacterAnimManager
    {
        EnemyLocoManager enemyLoco;
        private void Start()
        {
            anim = GetComponent<Animator>();
            enemyLoco = GetComponent<EnemyLocoManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLoco.enemyRB.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyLoco.enemyRB.velocity = velocity;
        }
    }
}
