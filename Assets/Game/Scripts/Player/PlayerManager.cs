using MumbaiChawls.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MumbaiChawls
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerLoco playerLoco;
        CameraHandler cameraHandler;
        
        Animator anim;


        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;


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
            anim = GetComponent<Animator>();            
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

            isInteracting = anim.GetBool(AnimHash.INTERACTING);
            
            inputHandler.TickInput(delta);            
            playerLoco.HandleMovement(delta);
            playerLoco.HandleRollingAndSprinting(delta);
            playerLoco.HandleFalling(delta, playerLoco.moveDirection);

        }
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isSprinting = inputHandler.b_Input;

            if(isInAir)
            {
                playerLoco.inAirTime = playerLoco.inAirTime + Time.deltaTime;
            }
        }
    }
}
