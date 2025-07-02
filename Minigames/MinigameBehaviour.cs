using System;
using UnityEngine;

namespace Minigames
{
    public enum MinigameState
    {
        Loading,
        Active,
        Fail,
        Success,
    }
    
    public abstract class MinigameBehaviour : MonoBehaviour
    {
        public MinigameState CurrentState { get; protected set; }
        
        public Action<MinigameState, MinigameState> OnStateChanged;

        private MinigameCamera _MinigameCamera;
        public MinigameCamera MinigameCamera => _MinigameCamera;

        public virtual void Awake()
        {
            CurrentState = MinigameState.Loading;
        }

        public virtual void Start()
        {
            CurrentState = MinigameState.Active;
        }
    }
}