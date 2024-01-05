using MumbaiChawls.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        public bool rb_Input;
        public bool rt_Input;
        public bool f_Input;

        public bool d_up;
        public bool d_right;
        public bool d_down;
        public bool d_left;

        public bool lockOnInput;
        
        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;


        

        public float rollInputTimer;

        Vector2 cameraInput;
        Vector2 movementInput;

        PlayerInputs inputActions;
        PlayerAttacker attacker;
        PlayerInventory inventory;
        PlayerManager playerManager;
        CameraHandler cameraHandler;
        private void Start()
        {
            attacker = GetComponent<PlayerAttacker>();
            inventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            cameraHandler = CameraHandler.Instance;
        }

        private void OnEnable ()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerInputs();
                inputActions.Player.move.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.Player.cam.performed += camActions => cameraInput = camActions.ReadValue<Vector2>();

                inputActions.PlayerAction.LockOn.performed += i => lockOnInput = true;
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleMoveInout(delta);
            HandleRollingInput(delta);
            HandleAttackInput(delta);
            HandleLockOnInput();
            HandleQuickSloteInput();
            HandleInteractableInput();
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
        private void HandleMoveInout(float delta)
        {
            horizontal = movementInput.x; 
            vertical = movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = cameraInput.x; 
            mouseY = cameraInput.y;
        }
        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerAction.RB.performed += i => rb_Input = true;
            inputActions.PlayerAction.RT.performed += i => rt_Input = true;

            if (rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    if (comboFlag) return;
                    comboFlag = true;
                    attacker.HandleWeaponCombo(inventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;
                    attacker.HandleLightAttack(inventory.rightWeapon);
                }
                
            }else if (rt_Input)
            {
                attacker.HandleHeavyAttack(inventory.rightWeapon);
            }
        }
        private void HandleLockOnInput()
        {
            if(lockOnInput && !lockOnFlag)
            {
                cameraHandler.ClearLockOnTargets();
                lockOnInput = false;               
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    cameraHandler.cinem.Play(AnimHash.LOCKED);
                    lockOnFlag = true;
                }

            }
            else if (lockOnInput && lockOnFlag)
            {
                lockOnFlag = false;
                lockOnInput = false;
                cameraHandler.cinem.Play(AnimHash.UNLOCKED);
                cameraHandler.ClearLockOnTargets();
            }
            cameraHandler.SetCameraHeight();
        }

        public void HandleQuickSloteInput()
        {
            inputActions.UICon.DRight.performed += i => d_right = true;
            inputActions.UICon.DUp.performed += i => d_up = true;

            if (d_up)
            {
                inventory.ChangeLeftWeapon();
            }
            else if (d_right)
            {
                inventory.ChangeRightWeapon();
            }
        }

        public void HandleInteractableInput()
        {
            inputActions.PlayerAction.Interact.performed += i => f_Input = true;
        }
    }
}

