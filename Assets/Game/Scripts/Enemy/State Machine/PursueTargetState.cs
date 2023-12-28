using MumbaiChawls.Core;
using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy
{
    public class PursueTargetState : State
    {
        public IdleState idleState;
        public CombatStanceState combatStanceState;
        public LayerMask detectionLayer;
        public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimManager animManager)
        {
            if (manager.isPerformingAction)
                return this;

            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);
            manager.viableAngle = Vector3.Angle(targetDirection, transform.forward);
            if (manager.distanceFromTarget > manager.maximumAttackRange)
            {
                animManager.anim.SetFloat(AnimHash.VERTICAL, 1, 0.1f, Time.deltaTime);
            }

            HandleRotationToTarget(manager);
            manager.agent.transform.localPosition = Vector3.zero;
            manager.agent.transform.localRotation = Quaternion.identity;


            #region  Handle Enemy Target Detection
            /*Collider[] colliders = Physics.OverlapSphere(transform.position, manager.detectionRadius, detectionLayer);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterStatsManager CSM = colliders[i].GetComponent<CharacterStatsManager>();
                    if (CSM != null)
                    {
                        //check for team id ????????????????????????????                        
                        Vector3 targetNewDirection = CSM.transform.position - transform.position;
                        float viableNewAngle = Vector3.Angle(targetNewDirection, transform.forward);

                        if (viableNewAngle > -manager.detectionAngle && viableNewAngle < manager.detectionAngle)
                        {
                            // Do nothing
                        }
                        else
                        {
                            manager.currentTarget = null;
                        }

                    }
                }
            }
            else
            {
                manager.currentTarget = null;
            }*/
            #endregion


            if (manager.currentTarget)
            {
                if (manager.distanceFromTarget <= manager.maximumAttackRange)
                {
                    return combatStanceState;
                }
            }
            else if (manager.currentTarget == null)
            {
                return idleState;
            }            
            else
            {
                return this;
            }
            return null;
        }

        public void HandleRotationToTarget(EnemyManager manager)
        {
            if (manager.isPerformingAction)
            {
                Vector3 direction = manager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                manager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, manager.rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(manager.agent.desiredVelocity);
                Vector3 targetVelocity = manager.enemyRB.velocity;

                manager.agent.enabled = true;
                manager.agent.SetDestination(manager.currentTarget.transform.position);
                manager.enemyRB.velocity = targetVelocity;
                manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, manager.agent.transform.rotation, manager.rotationSpeed / Time.deltaTime);
            }


        }
    }
}
