using UnityEngine;

namespace MumbaiChawls
{
    public class AnimHash
    {
        public const string HORIZONTAL = "horizontal";
        public const string VERTICAL = "vertical";

        public const string LOCOMOTION = "Locomotion";

        public const string INTERACTING = "isInteracting";

        public const string ROLLING = "Stand To Roll";
        public const string BACKSTAB = "Jump Backward";

        public const string FALLING = "Falling Idle";
        public const string LAND = "Falling to land";
        public AnimHash()
        {
            Animator.StringToHash(LOCOMOTION);
            Animator.StringToHash(HORIZONTAL);
            Animator.StringToHash(VERTICAL);
            Animator.StringToHash(INTERACTING);
            Animator.StringToHash(ROLLING);
            Animator.StringToHash(BACKSTAB);
            Animator.StringToHash(FALLING);
            Animator.StringToHash(LAND);
        }
    }
}

