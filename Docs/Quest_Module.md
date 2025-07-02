## README: Quest Module

### Структура
- **QuestService.cs** — главный сервис, ведёт список квестов    
- **Quest.cs** — содержит задачи (объекты, реализующие `ITaskCondition`)    
- **ITaskCondition.cs** — интерфейс, для условий квестов    

### Интеграция
- Добавьте `QuestService` на сцену    
- Квесты (объект Quest) должны быть указаны в поле `quests`    
- Для условий задач реализуйте ITaskCondition    

### Пример: собственное условие задачи

```cs
public class TestQuestCondition : MonoBehaviour, ITaskCondition
{
    public event Action<ITaskCondition> onTaskCompleted;

    private void Update()
    {
        if (transform.position.y > 10f)
        {
            onTaskCompleted?.Invoke(this);
        }
    }
}
```

Добавьте этот компонент в список `questObjects` в `Quest`, чтобы он отслеживался как шаг квеста.