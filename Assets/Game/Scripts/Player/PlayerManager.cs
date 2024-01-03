using MumbaiChawls.Core;
using MumbaiChawls.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MumbaiChawls
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        PlayerLoco playerLoco;
        CameraHandler cameraHandler;
        PlayerAnimHandler playerAnimHandler;
        //Animator anim;


        public bool isInteracting;


        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;


        private void Awake()
        {
            cameraHandler = CameraHandler.Instance;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLoco = GetComponent<PlayerLoco>();
            playerAnimHandler = GetComponent<PlayerAnimHandler>();          
        }
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (cameraHandler)
            {
                cameraHandler.followTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void Update()
        {
            float delta = Time.deltaTime; 

            isInteracting = playerAnimHandler.anim.GetBool(AnimHash.INTERACTING);
            canDoCombo = playerAnimHandler.anim.GetBool(AnimHash.CANDOCOMBO);
            
            inputHandler.TickInput(delta);            
            playerLoco.HandleMovementInput(delta);
            playerLoco.HandleRollingAndSprinting(delta);
            playerLoco.HandleFalling(delta, playerLoco.moveDirection);

            CheckForInteractableOnject();

        }
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;

            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;

            inputHandler.d_up = false;
            inputHandler.d_right = false;

            inputHandler.f_Input = false;

            if(isInAir)
            {
                playerLoco.inAirTime = playerLoco.inAirTime + Time.deltaTime;
            }
        }

        public void CheckForInteractableOnject()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.6f, transform.forward, out hit, 1f, cameraHandler.ignoreLayer))
            {
                if(hit.collider.tag == TagHash.INTERACTABLE)
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactbleText;

                        if (inputHandler.f_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
        }

        public void InteractWithFirecracker(Transform t, string anim)
        {
            playerLoco.rigidbody.velocity = Vector3.zero;
            //transform.position = t.position;
            playerAnimHandler.PlayTargetAnimation(anim, true);
        }
    }
}
