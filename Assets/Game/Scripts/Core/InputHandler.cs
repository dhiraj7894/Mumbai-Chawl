using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

namespace MumbaiChawls
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal; 
        public float vertical;

        public float moveAmount;

        public float mouseX;
        public float mouseY;

        public bool b_Input;
        
        public bool rollFlag;
        public bool sprintFlag;

        

        public float rollInputTimer;

        Vector2 cameraInput;
        Vector2 movementInput;

        PlayerInputs inputActions;

        private void OnEnable ()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerInputs();
                inputActions.Player.move.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.Player.cam.performed += camActions => cameraInput = camActions.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            Move(delta);
            HandleRollingInput(delta);
        }

        public void HandleRollingInput(float delta)
        {
            b_Input = inputActions.PlayerAction.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }else
            {
                if(rollInputTimer>0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }
        private void Move(float delta)
        {
            horizontal = movementInput.x; 
            vertical = movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = cameraInput.x; 
            mouseY = cameraInput.y;
        }
    }
}

