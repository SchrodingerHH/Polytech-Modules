## README: Minigame Module

### Структура
- **MinigameObject.cs** — загружает сцену мини-игры и отслеживает её статус    
- **MinigameService.cs** — собирает все мини-игры, переключает управление    
- **MinigameStartTrigger.cs** — интерактив, для запуска мини-игры    

### Интеграция
- Наследуйте `MinigameBehaviour` и устанавливайте `CurrentState`    
- На сцене с мини-игрой должен быть только один `MinigameBehaviour`    
- Подключайте игру через `MinigameStartTrigger`, он подгрузит сцену    

### Пример: простая мини-игра с таймером

```cs
public class TestMinigame : MinigameBehaviour
{
    private float timer = 5f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CurrentState = MinigameState.Success;
            OnStateChanged?.Invoke(MinigameState.Active, MinigameState.Success);
        }
    }
}
```

Добавьте этот скрипт на объект в сцене мини-игры. Сцена будет завершена успешно через 5 секунд.

Созависимость:
- От [[Quest Module]] т.к. `MinigameObject` реализует интерфейс `ITaskCondition`
- От [[Inventory Module]] т.к. `MinigameStartTrigger` имеет условие старта если список предметов необходимых для головоломки пуст