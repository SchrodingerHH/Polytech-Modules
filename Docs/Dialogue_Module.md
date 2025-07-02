## README: Dialogue Module

### Структура
- **DialogueService.cs** — загрузка Ink и обработка диалогов
- **DialogueCharacterTrigger.cs** — связан с нпс, запускает диалог при Interact()

### Интеграция
- Добавьте `DialogueService` на сцену
- NPC объекты с `DialogueCharacterTrigger` должны иметь Ink TextAsset
- Игрок должен взаимодействовать с NPC через `IInteractible`
- UI обновляется через события `onDialogueLineUpdate`, `onChoicesUpdate` (тип: `Action<string>` и `Action<Dictionary<int, string>>` соответственно; подпишитесь через `+=` или `Subscribe`, чтобы получать строки диалога и варианты выбора)

#### Пример:
`DialogueService` предоставляет два ключевых события:
- `onDialogueLineUpdate` — обновление текущей строки диалога (тип `Action<string>`)
- `onChoicesUpdate` — список вариантов выбора (тип `Action<Dictionary<int, string>>`)

```cs
private void OnEnable()
{
    dialogueService.onDialogueLineUpdate += OnLine;
    dialogueService.onChoicesUpdate += OnChoices;
}

private void OnDisable()
{
    dialogueService.onDialogueLineUpdate -= OnLine;
    dialogueService.onChoicesUpdate -= OnChoices;
}

private void OnLine(string text) => Debug.Log($"Диалог: {text}");
private void OnChoices(Dictionary<int, string> choices)
{
    foreach (var c in choices)
        Debug.Log($"Выбор {c.Key}: {c.Value}");

    // 💡 Индекс (ключ словаря) можно использовать для выбора:
    if (choices.ContainsKey(0))
        dialogueService.MakeChoice(0); // делает выбор по индексу
}
```