using System;
using UnityEngine;

namespace Quest
{
    public class TestQuestCondition : MonoBehaviour, ITaskCondition
    {
        public event Action<ITaskCondition> onTaskCompleted;

        private void Update()
        {
            if (transform.position.y>10f)
            {
                onTaskCompleted?.Invoke(this);
            }
        }
    }
}