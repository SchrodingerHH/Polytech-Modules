using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;
using Quest;

namespace Minigames
{
    public class MinigameObject : MonoBehaviour, ITaskCondition
    {
        [SerializeField]
        private SceneReference _sceneReference;

        private Scene _currentScene;

        public MinigameBehaviour MinigameBehaviour { get; private set; }

        public event Action<ITaskCondition> onTaskCompleted;
        public event Action<MinigameObject> OnMinigameStarted;
        public event Action<MinigameObject> OnMinigameCompleted;

        [Button]
        public void Load()
        {
            //var asyncOp = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            //var asyncOp = SceneManager.LoadSceneAsync(_sceneReference.Path, LoadSceneMode.Additive);
            Addressables.LoadSceneAsync(_sceneReference.Address, LoadSceneMode.Additive)
                .Completed += OnLoadComplete;
        }

        private void OnLoadComplete(AsyncOperationHandle<SceneInstance> asyncOperation)
        {
            //TODO: Error handling when MinigameBehaviour not found
            //_currentScene = SceneManager.GetSceneByPath(_sceneReference.Path);
            _currentScene = asyncOperation.Result.Scene;
            SceneManager.SetActiveScene(_currentScene);
            MinigameBehaviour = FindFirstObjectByType<MinigameBehaviour>();
            MinigameBehaviour.OnStateChanged += MinigameBehaviourOnOnStateChanged;
            
            OffsetSceneObjects(_currentScene, Vector3.right*200);
        }
        
        private void Unload()
        {
            var asyncOp = SceneManager.UnloadSceneAsync(_currentScene);
            asyncOp.completed += OnUnloadComplete;
            //TODO: Safe async
        }

        private void OnUnloadComplete(AsyncOperation asyncOperation)
        {
            return;
        }
        
        private void OffsetSceneObjects(Scene scene, Vector3 offset)
        {
            foreach (GameObject rootObject in scene.GetRootGameObjects())
            {
                rootObject.transform.position += offset;
            }
        }

        private void MinigameBehaviourOnOnStateChanged(MinigameState previousState, MinigameState newState)
        {
            switch (newState)
            {
                case MinigameState.Success:
                    onTaskCompleted?.Invoke(this);
                    OnMinigameCompleted?.Invoke(null); //TODO: Refactor
                    Unload();
                    break;
                case MinigameState.Fail:
                    OnMinigameCompleted?.Invoke(null);
                    Unload();
                    break;
                case MinigameState.Active:
                    OnMinigameStarted?.Invoke(this);
                    break;
            }
        }
    }
}