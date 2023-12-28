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
        EnemyManager manager;
        EnemyAnimManager enemyAnimManager;

        private void Start()
        {
           
            manager = GetComponent<EnemyManager>();
            enemyAnimManager = GetComponent<EnemyAnimManager>();
        }
        
    }
}
