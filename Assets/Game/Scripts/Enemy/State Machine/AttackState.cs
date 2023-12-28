using MumbaiChawls.Control;
using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MumbaiChawls.Enemy
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;

        public EnemyAttackAction[] attackActions;
        public EnemyAttackAction currentAttack;

        public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimManager animManager)
        {
            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);
            manager.viableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (manager.isPerformingAction) 
                return combatStanceState;

           
            Debug.Log("WHY ?");
            
            if (currentAttack != null)
            {
                if(manager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                else if(manager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
                {
                    if (manager.viableAngle<= currentAttack.attackAngle && 
                        manager.viableAngle >= -currentAttack.attackAngle)
                    {
                        if (manager.currentRecoveryTime <= 0 && !manager.isPerformingAction)
                        {
                            animManager.anim.SetFloat(AnimHash.VERTICAL,0,0.1f,Time.deltaTime);
                            animManager.anim.SetFloat(AnimHash.HORIZONTAL,0,0.1f,Time.deltaTime);
                            animManager.PlayTargetAnimation(currentAttack.actionAnimation, true);

                            manager.isPerformingAction = true;
                            manager.currentRecoveryTime = currentAttack.recoveryTime;

                            currentAttack = null;
                            return combatStanceState;
                        }
                        
                    }
                }
                
            }
            else
            {
                GetNewAttack(manager);
            }
            return combatStanceState;
        }
        public void GetNewAttack(EnemyManager manager)
        {
            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            float viableAngle = Vector3.Angle(targetDirection, transform.forward);
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < attackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = attackActions[i];

                if (manager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    &&
                    manager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viableAngle <= enemyAttackAction.attackAngle && viableAngle >= -enemyAttackAction.attackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;

                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < attackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = attackActions[i];
                if (manager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    &&
                    manager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viableAngle <= enemyAttackAction.attackAngle && viableAngle >= -enemyAttackAction.attackAngle)
                    {
                        if (currentAttack != null)
                        {
                            return;
                        }
                        temporaryScore += enemyAttackAction.attackScore;
                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }

                    }
                }
            }
        }

    }
}
