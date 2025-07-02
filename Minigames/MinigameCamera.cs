using UnityEngine;

namespace Minigames
{
    [RequireComponent(typeof(Camera))]
    public class MinigameCamera : MonoBehaviour
    {
        public Camera Camera {get; private set;}
        
        private void Awake()
        {
            Camera = GetComponent<Camera>();
            //Camera.enabled = false;
        }
    }
}