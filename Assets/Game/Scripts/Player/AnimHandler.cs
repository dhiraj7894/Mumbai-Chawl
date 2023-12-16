using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MumbaiChawls.Player
{
    public class AnimHandler : MonoBehaviour
    {
        PlayerManager playerManager;
        InputHandler inputHandler;
        PlayerLoco playerLoco;
        public Animator anim;

        int horizontal;
        int vertical;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            playerLoco = GetComponent<PlayerLoco>();
            playerManager = GetComponent<PlayerManager>();
        }
        public void UpdateAnimatorValues(float vericalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;
            if (vericalMovement > 0 && vericalMovement < 0.55f)
            {
                v = .5f;
            }
            else if (vericalMovement > 0.55f)
            {
                v = 1;
            }
            else if (vericalMovement < 0 && vericalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (vericalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = .5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(AnimHash.VERTICAL, v, 0.1f, Time.deltaTime);
            anim.SetFloat(AnimHash.HORIZONTAL, h, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool(AnimHash.INTERACTING, isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (!playerManager.isInteracting)
                return;
            float delta = Time.deltaTime;
            playerLoco.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLoco.rigidbody.velocity = velocity;
        }
    }
}

