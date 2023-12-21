using MumbaiChawls.Player;
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
        public bool rb_Input;
        public bool rt_Input;
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
            Move(delta);
            HandleRollingInput(delta);
            HandleAttackInput(delta);
            HandleLockOnInput();
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
                    attacker.HandleWeaponCombo(inventory.rightCombatItem);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;
                    attacker.HandleLightAttack(inventory.rightCombatItem);
                }
                
            }else if (rt_Input)
            {
                attacker.HandleHeavyAttack(inventory.rightCombatItem);
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
                    lockOnFlag = true;
                }

            }
            else if (lockOnInput && lockOnFlag)
            {
                lockOnFlag = false;
                lockOnInput = false;   
                cameraHandler.ClearLockOnTargets();
            }            
        }
    }
}

