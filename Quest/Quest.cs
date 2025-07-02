using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] 
        private List<GameObject> questObjects;
    
        private List<ITaskCondition> tasks;

        private HashSet<ITaskCondition> completedTasks;
    
        public bool IsCompleted { get; private set; }
        
        public event Action onQuestCompleted;

        private void Awake()
        {
            completedTasks = new HashSet<ITaskCondition>();
            tasks = new List<ITaskCondition>(questObjects.Count);
        }
    
        private void Start()
        {
            foreach (var questObject in questObjects)
            {
                if (questObject.TryGetComponent<ITaskCondition>(out var condition))
                {
                    tasks.Add(condition);
                    condition.onTaskCompleted += ConditionOnTaskCompleted;
                }
            }
        }

        private void ConditionOnTaskCompleted(ITaskCondition sender)
        {
            completedTasks.Add(sender);
            sender.onTaskCompleted -= ConditionOnTaskCompleted;

            if (completedTasks.Count == tasks.Count)
            {
                IsCompleted = true;
                onQuestCompleted?.Invoke();
            }
        }
    }
}