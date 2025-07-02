using System;
using UnityEngine;
using UnityEngine.Serialization;
using Cysharp.Threading.Tasks;
using Zenject;
using Interactibles;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region References
        
        //Object references
        public PlayerInputHandler InputHandler;
        
        [SerializeField]
        private CameraController _cameraController;
        private Rigidbody _rigidbody;
        
        #endregion

        #region SettingValues

        //Controls settings
        [SerializeField]
        private float interactDistance = 2;
        [SerializeField]
        private float moveSpeed = 1;

        //Socket positions
        public Vector3 rootSocket;
        public Vector3 cameraSocket;

        #endregion

        #region InternalValues
        
        private float _velocityY;
        private bool _isGrounded;

        #endregion

        [Inject]
        private async void Construct(PlayerInputHandler inputHandler)
        {
            InputHandler = inputHandler;
            
            await UniTask.WaitForEndOfFrame();
            
            InputHandler.Interact += Interact;
            
            inputHandler.SwitchControlsMap(PlayerInputHandler.ControlsMap.Player);
            inputHandler.CursorState(true);
        }

        private void OnDestroy() => InputHandler.Interact -= Interact;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            IsGrounded();
        }

        private void Update()
        {
            Move();
            Look();
            _cameraController.FollowTarget(transform.position+cameraSocket);
        }
        
        private void IsGrounded() => _isGrounded = Physics.CheckSphere(transform.position+rootSocket, 0.2f);

        private void Move()
        {
            Vector2 inputDir = InputHandler.GetMoveAxis();
            Vector3 horizontalVelocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * moveSpeed;
            
            if (_isGrounded && _velocityY < 0f)
            {
                _velocityY = -2f;
            }
            else
            {
                _velocityY += Physics.gravity.y * Time.deltaTime;
            }

            Vector3 velocity = new Vector3(horizontalVelocity.x, _velocityY, horizontalVelocity.z);
            _rigidbody.linearVelocity = velocity;
        }
        
        private void Look()
        {
            Vector2 mouseDelta = InputHandler.GetLookAxis();
            
            _rigidbody.MoveRotation(transform.rotation * Quaternion.Euler(0, mouseDelta.x, 0));
            
            _cameraController.Rotate(transform, mouseDelta);
        }
        
        private void Interact()
        {
            Ray ray = new Ray(_cameraController.transform.position,_cameraController.transform.forward * interactDistance);
            if (Physics.Raycast(ray, out RaycastHit rayHit, interactDistance))
            {
                GameObject selectedObject = rayHit.collider.gameObject;
                selectedObject.GetComponent<IInteractible>()?.Interact();
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position+cameraSocket, 0.2f);
            Gizmos.DrawWireSphere(transform.position+rootSocket, 0.2f);
            Gizmos.DrawLine(transform.position+cameraSocket, transform.position+cameraSocket+(Vector3.forward*interactDistance));
            if (_cameraController != null)
            {
                Gizmos.DrawLine(_cameraController.transform.position, _cameraController.transform.forward*interactDistance);
            }
        }
#endif
    }
}