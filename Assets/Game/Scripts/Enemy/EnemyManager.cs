using MumbaiChawls.Control;
using MumbaiChawls.Core;
using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MumbaiChawls.Enemy
{
    public class EnemyManager : CharacterManager
    {
        EnemyAnimManager animManager;
        EnemyStats enemyState;

        public NavMeshAgent agent;
        public Rigidbody enemyRB;


        public State currentState;

        public bool isPerformingAction;

       /* public EnemyAttackAction[] attackActions;
        public EnemyAttackAction currentAttack;*/

        [Header("A.I. Setting")]
        public CharacterStatsManager currentTarget;        

        public float distanceFromTarget;        
        public float rotationSpeed = 15;
        public float maximumAttackRange = 1.25f;
        public float detectionRadius = 30;
        public float viableAngle = 50;
        public float detectionAngle = 50;



        public float currentRecoveryTime = 0;
            
        private void Awake()
        {

            animManager = GetComponent<EnemyAnimManager>();
            enemyState = GetComponent<EnemyStats>();
            agent = GetComponentInChildren<NavMeshAgent>();
            enemyRB = GetComponent<Rigidbody>();

            agent.enabled = false;
            
        }
        private void Start()
        {            
            enemyRB.isKinematic = false;
        }
        private void Update()
        {
            HandleRecoveryTimer();
        }
        private void FixedUpdate()
        {
            HandleStateMachine();
        }
        private void HandleStateMachine()
        {               
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyState, animManager);
                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
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
















#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawGizmos(detectionRadius, new Color32(255, 255, 255, 10));
        }
#endif
    }
}
