using MumbaiChawls.Enemy.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Enemy
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimManager animManager);
    }
}
