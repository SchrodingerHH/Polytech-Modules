using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerInputHandler : IDisposable
    {
        private readonly PlayerInput _playerInput;
        
        private Vector2 _inputVector;
        private Vector2 _intendedMoveVector;
        private Vector2 _intendedLookVector;

        public bool isMouseMovementDisabled = false;
        private bool _isCursorLocked;

        public event Action Interact;
        
        public enum ControlsMap
        {
            Player,
            UI,
        }
        
        public PlayerInputHandler(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            Subscribe();
        }

        public void ControlsState(bool isEnabled)
        {
            switch (isEnabled)
            {
                case true: 
                    _playerInput.actions.Enable();
                    break;
                case false: 
                    _playerInput.actions.Disable();
                    break;
            }
        }
        
        public void CursorState(bool isLocked)
        {
            switch (isLocked)
            {
                case true:
                    _isCursorLocked = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    _playerInput.actions["Look"].Disable();
                    break;
                case false:
                    _isCursorLocked = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    _playerInput.actions["Look"].Enable();
                    break;
            }
        }
        
        public void SwitchControlsMap(ControlsMap map)
        {
            switch (map)
            {
                case ControlsMap.Player:
                    _playerInput.SwitchCurrentActionMap("Player");
                    break;
                case ControlsMap.UI:
                    _playerInput.SwitchCurrentActionMap("UI");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(map), map, null);
            }
        }

        public void Subscribe()
        {
            _playerInput.actions["CursorLock"].performed += OnCursorStateSwitch;
            
            _playerInput.actions["Move"].started += GetMoveVector;
            _playerInput.actions["Move"].performed += GetMoveVector;
            _playerInput.actions["Move"].canceled += GetMoveVector;

            _playerInput.actions["Look"].started += GetLookVector;
            _playerInput.actions["Look"].performed += GetLookVector;
            _playerInput.actions["Look"].canceled += GetLookVector;
            
            _playerInput.actions["Interact"].performed += OnInteract;
        }

        private void OnCursorStateSwitch(InputAction.CallbackContext ctx) => CursorState(!_isCursorLocked);

        private void OnInteract(InputAction.CallbackContext ctx) => Interact?.Invoke();

        private void GetMoveVector(InputAction.CallbackContext ctx) => _intendedMoveVector = ctx.ReadValue<Vector2>();

        private void GetLookVector(InputAction.CallbackContext ctx) => _intendedLookVector = ctx.ReadValue<Vector2>();

        public Vector2 GetMoveAxis() => _intendedMoveVector;

        public Vector2 GetLookAxis()
        {
            if (isMouseMovementDisabled)
            {
                return Vector2.zero;
            }
            else
            {
                return _intendedLookVector;
            }
        }
        
        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _playerInput.actions["Move"].started -= GetMoveVector;
            _playerInput.actions["Move"].performed -= GetMoveVector;
            _playerInput.actions["Move"].canceled -= GetMoveVector;
            
            _playerInput.actions["Look"].started -= GetLookVector;
            _playerInput.actions["Look"].performed -= GetLookVector;
            _playerInput.actions["Look"].canceled -= GetLookVector;
            
            _playerInput.actions["Interact"].performed -= OnInteract;
        }
    }
}