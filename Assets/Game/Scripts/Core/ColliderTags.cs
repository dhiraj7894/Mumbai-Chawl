using UnityEngine;

namespace MumbaiChawls
{
    public class ColliderTags
    {
        public const string PLAYER = "player";
        public const string ENEMY = "enemy";
        public ColliderTags()
        {
            Animator.StringToHash(PLAYER);
            Animator.StringToHash(ENEMY);
        }
    }
}

