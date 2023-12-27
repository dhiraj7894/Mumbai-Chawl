using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MumbaiChawls.Core;
using UnityEngine.AI;
using System.Data;

namespace MumbaiChawls.Enemy
{
    public class EnemyLocoManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimManager enemyAnimManager;
        public Rigidbody enemyRB;

        [HideInInspector]
        public NavMeshAgent agent;

        public CharacterStatsManager currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 3;
        public float rotationSpeed = 15;
        private void Start()
        {
            enemyRB = GetComponent<Rigidbody>();
            agent = GetComponentInChildren<NavMeshAgent>();
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimManager = GetComponent<EnemyAnimManager>();

            agent.enabled = false;
            enemyRB.isKinematic = false;
        }
        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
            if(colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterStatsManager CSM = colliders[i].GetComponent<CharacterStatsManager>();
                    if (CSM != null)
                    {
                        //check for team id ????????????????????????????                        
                        Vector3 targetDirection = CSM.transform.position - transform.position;
                        float viableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viableAngle > -enemyManager.detectionAngle && viableAngle < enemyManager.detectionAngle)
                        {
                            currentTarget = CSM;
                        }
                        else
                        {
                            //currentTarget = null;
                        }
                    }
                }
            }
            else
            {
                //currentTarget = null;
            }
                             
        }

        public void HandleMoveToTarget()
        {
            if (enemyManager.isPerformingAction) 
                return;

            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            float viableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPerformingAction)
            {
                enemyAnimManager.anim.SetFloat(AnimHash.VERTICAL, 0, 0.1f, Time.deltaTime);
                agent.enabled = false;
            }
            else
            {
                if(distanceFromTarget > stoppingDistance)
                {
                    enemyAnimManager.anim.SetFloat(AnimHash.VERTICAL, 1, 0.1f, Time.deltaTime);
                }
                else
                {
                    enemyAnimManager.anim.SetFloat(AnimHash.VERTICAL, 0, 0.1f, Time.deltaTime);
                }
            }
            HandleRotationToTarget();
        }

        public void HandleRotationToTarget()
        {
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(agent.desiredVelocity);
                Vector3 targetVelocity = enemyRB.velocity;

                agent.enabled = true;
                agent.SetDestination(currentTarget.transform.position);
                enemyRB.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, agent.transform.rotation, rotationSpeed);    
            }
            agent.transform.localPosition = Vector3.zero;
            agent.transform.localRotation = Quaternion.identity;
        }
    }
}
