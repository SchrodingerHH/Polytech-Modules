using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Minigames
{
    public class MainsceneCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        private UniversalAdditionalCameraData _cameraData;

        [SerializeField] 
        private MinigameService minigameService;
        

        private void Awake()
        {
            _cameraData = _camera.GetUniversalAdditionalCameraData();
            
            minigameService.CurrentActiveMinigameControllerChanged += MinigameServiceOnCurrentActiveMinigameControllerChanged;
        }

        private void MinigameServiceOnCurrentActiveMinigameControllerChanged(MinigameObject minigame)
        {
            if (minigame != null)
            {
                var minigameCamera = minigame.MinigameBehaviour.MinigameCamera;
                _camera.gameObject.SetActive(false);
            }
            else
                _camera.gameObject.SetActive(true);
            
            /*if (minigame != null) //TODO: Switch camera instead of overlay
            {
                var minigameCamera = minigame.MinigameBehaviour.MinigameCamera;
                _cameraData.cameraStack.Add(minigameCamera.Camera);
            }
            else
            {
                _cameraData.cameraStack.Clear();
            }*/
        }
    }
}