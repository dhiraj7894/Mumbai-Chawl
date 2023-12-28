using MumbaiChawls.Core;
using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;
        public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimManager animManager)
        {
            //look for target
            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, manager.detectionRadius, detectionLayer);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterStatsManager CSM = colliders[i].GetComponent<CharacterStatsManager>();
                    if (CSM != null)
                    {
                        //check for team id ????????????????????????????                        
                        Vector3 targetDirection = CSM.transform.position - transform.position;
                        float viableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viableAngle > -manager.detectionAngle && viableAngle < manager.detectionAngle)
                        {
                            manager.currentTarget = CSM;                            
                        }

                    }
                }
            }

            #endregion

            #region Handle Switch To Pursue State
            if (manager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
