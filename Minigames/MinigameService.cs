using System;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Zenject;

namespace Minigames
{
    public class MinigameService : MonoBehaviour
    {
        private List<MinigameObject> _minigameObjects;
        
        public MinigameObject currentActiveMinigameObject {get; private set;}
        
        public event Action<MinigameObject> CurrentActiveMinigameControllerChanged;

        private PlayerInputHandler _playerInputHandler;
        
        [Inject]
        private void Construct(PlayerInputHandler inputHandler)
        {
            _playerInputHandler = inputHandler;
        }
        
        private void Awake()
        {
        
            var minigames = FindObjectsByType<MinigameObject>(FindObjectsSortMode.None);
            _minigameObjects = new List<MinigameObject>(minigames);
        }

        private void Start()
        {
            foreach (var controller in _minigameObjects)
            {
                controller.OnMinigameStarted += OnMinigameStarted;
                controller.OnMinigameCompleted += OnMinigameCompleted;
            }
        }

        private void OnMinigameStarted(MinigameObject sender)
        {
            _playerInputHandler.SwitchControlsMap(PlayerInputHandler.ControlsMap.UI);
            _playerInputHandler.CursorState(false);

            currentActiveMinigameObject = sender;
            CurrentActiveMinigameControllerChanged?.Invoke(sender);
        }
        
        private void OnMinigameCompleted(MinigameObject sender)
        {
            _playerInputHandler.SwitchControlsMap(PlayerInputHandler.ControlsMap.Player);
            _playerInputHandler.CursorState(true);

            currentActiveMinigameObject = null;
            CurrentActiveMinigameControllerChanged?.Invoke(null);
        }
    }
}