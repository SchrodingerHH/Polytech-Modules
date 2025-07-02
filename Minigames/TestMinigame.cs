using System;
using ModAPI;
using UnityEngine;

namespace Minigames
{
    public class TestMinigame : MinigameBehaviour
    {
        private float _timer = 10f;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0) 
            {
                CurrentState = MinigameState.Success;
                OnStateChanged?.Invoke(CurrentState, CurrentState);
            }
        }
    }
}