# Polytech-Modules
### Требуемые зависимости

Все модули используют [Zenject](https://github.com/Mathijs-Bakker/Extenject) для внедрения зависимостей (Dependency Injection).

Чтобы корректно использовать все сервисы и взаимодействия между ними (например, `InventoryService`, `DialogueService`, `PlayerInputHandler` и др.), необходимо:

1. Установить Zenject через Unity Package Manager или скачав `.unitypackage` с GitHub.
2. Добавить соответствующие `Installer`-классы на сцену или в префабы, если требуется настраивать зависимые компоненты.

### Пример SceneInstaller

```cs
using Dialogue;
using Inventory;
using Minigames;
using Player;
using Quest;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInput playerInput;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInputHandler>()
            .AsSingle()
            .WithArguments(playerInput);

        Container.Bind<InventoryService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogueService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MinigameService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<QuestService>().FromComponentInHierarchy().AsSingle();
    }
}
```

> ⚠️ Обязательно поместите этот скрипт на объект в сцене с компонентом `Scene Context`.

### Кастомизация и модификации
Исходный код всех модулей является открытым и может быть изменён в соответствии с требованиями конкретного проекта:
- Вы можете заменить Zenject на любой другой способ инициализации (например, ручное связывание объектов)
- Логику поведения компонентов (например, условия завершения квеста или интерфейс мини-игры) можно адаптировать под нужды вашей игры
- UI-слой также не является жёстко привязанным к коду и может быть заменён

### Рекомендации
- Используйте `IInteractible` интерфейс для универсальных интеракций с объектами
- Не создавайте квесты и мини-игры динамически без регистрации их в сервисах (`QuestService`, `MinigameService`)
- Разделяйте слои данных (SO, модели), логики (сервисы) и UI (контроллеры) для масштабируемости проекта

### Список модулей
- [Quest Module](Docs/Quest_Module.md)
- [Minigame Module](Docs/Minigame_Module.md)
- [Inventory Module](Docs/Inventory_Module.md)
- [Dialogue Module](Docs/Dialogue_Module.md)
