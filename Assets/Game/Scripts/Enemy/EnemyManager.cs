using MumbaiChawls.Control;
using MumbaiChawls.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocoManager enemyLocoManager;
        EnemyAnimManager animManager;
        
        public bool isPerformingAction;

        public EnemyAttackAction[] attackActions;
        public EnemyAttackAction currentAttack;
        [Header("A.I. Setting")]
        public float detectionRadius = 30;
        public int detectionAngle = 50;


        public float currentRecoveryTime = 0;
            
        private void Awake()
        {
            
        }
        private void Start()
        {
            enemyLocoManager = GetComponent<EnemyLocoManager>();
            animManager = GetComponent<EnemyAnimManager>();
            StartCoroutine(HandleDetection(1));
        }
        private void Update()
        {
            HandleRecoveryTimer();
        }
        private void FixedUpdate()
        {
            HandleCurrentAction();
        }
        private void HandleCurrentAction()
        {            
            if (enemyLocoManager.currentTarget != null)
            {
                enemyLocoManager.distanceFromTarget = Vector3.Distance(enemyLocoManager.currentTarget.transform.position, transform.position);
                if (enemyLocoManager.distanceFromTarget > enemyLocoManager.stoppingDistance)
                {
                    Debug.Log("HUH ?");
                    enemyLocoManager.HandleMoveToTarget();
                }
                else if (enemyLocoManager.distanceFromTarget <= enemyLocoManager.stoppingDistance)
                {                    
                    AttackTarget();
                }

            }
            else
            {
                enemyLocoManager.agent.enabled = false;
            }
        }
        IEnumerator HandleDetection(float time)
        {
            while (true)
            {
                yield return new WaitForSeconds(time);
                enemyLocoManager.HandleDetection();
            }
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if(isPerformingAction)
            {
                if(currentRecoveryTime < 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        #region Attacks
        public void AttackTarget()
        {
            if (isPerformingAction) return;
            Debug.Log("WHY ?");
            if (currentAttack == null)
            {
                GetNewAttack();
            }
            else
            {
                isPerformingAction = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                animManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }

        public void GetNewAttack()
        {
            Vector3 targetDirection = enemyLocoManager.currentTarget.transform.position - transform.position;
            float viableAngle = Vector3.Angle(targetDirection, transform.forward);
            enemyLocoManager.distanceFromTarget = Vector3.Distance(enemyLocoManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < attackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = attackActions[i];
                if(enemyLocoManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack 
                    && 
                    enemyLocoManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
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
                if (enemyLocoManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    &&
                    enemyLocoManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viableAngle <= enemyAttackAction.attackAngle && viableAngle >= -enemyAttackAction.attackAngle)
                    {
                       if(currentAttack != null)
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

        #endregion














#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawGizmos(detectionRadius, new Color32(255, 255, 255, 10));
        }
#endif
    }
}
