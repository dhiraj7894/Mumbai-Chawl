using UnityEngine;

namespace MumbaiChawls.Control
{
    [CreateAssetMenu(menuName ="AI/Enemy Actions/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {
        public int attackScore = 3;
        public float recoveryTime = 2;

        public float attackAngle = 35;

        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 3;
    }
}