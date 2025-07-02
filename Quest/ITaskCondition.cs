using System;

namespace Quest
{
    public interface ITaskCondition
    {
        public event Action<ITaskCondition> onTaskCompleted;
    }
}