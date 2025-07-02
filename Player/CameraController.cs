using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        public Camera _camera;
        
        [Header("Controls settings",order = 0)]
        [SerializeField] 
        public float mouseSensitivity = 1;
        [Range(0, 180), SerializeField]
        public float verticalCameraClamp = 90;

        private float cameraPitch;
        public void FollowTarget(Transform cameraSocket)
        {
            transform.position = cameraSocket.position;
        }
        
        public void FollowTarget(Vector3 cameraSocket)
        {
            transform.position = cameraSocket;
        }

        public void Rotate(Transform playerTransform, Vector2 mouseDelta)
        {
            //Base camera rotation derived from playerTransform.up
            cameraPitch -= mouseDelta.y;
            cameraPitch = Mathf.Clamp(cameraPitch, -verticalCameraClamp, verticalCameraClamp);
            Quaternion rotation = Quaternion.Euler(cameraPitch, playerTransform.eulerAngles.y, 0f);
            
            transform.localRotation = rotation;
        }
    }
}