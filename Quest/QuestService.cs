using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestService : MonoBehaviour
    {
        public IReadOnlyList<Quest> Quests => quests;

        public int QuestIndex { get; private set; }
        
        [SerializeField]
        private List<Quest> quests;

        private void Awake()
        {
            quests[QuestIndex].onQuestCompleted += OnQuestCompleted;

            void OnQuestCompleted()
            {
                quests[QuestIndex].onQuestCompleted -= OnQuestCompleted;
                QuestIndex++;
            }
        }
    }
}