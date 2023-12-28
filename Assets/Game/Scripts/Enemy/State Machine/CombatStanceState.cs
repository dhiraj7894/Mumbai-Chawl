using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimManager animManager)
        {
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, manager.transform.position);

            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);
            manager.viableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (manager.currentRecoveryTime <= 0 && manager.distanceFromTarget <= manager.maximumAttackRange)
            {
                return attackState;
            }
            else if (manager.distanceFromTarget > manager.maximumAttackRange)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }            
        }
    }
}
