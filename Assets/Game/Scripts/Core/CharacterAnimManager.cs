using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Core
{
    public class CharacterAnimManager : MonoBehaviour
    {
        public Animator anim;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool(AnimHash.INTERACTING, isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

    }
}