## README: Inventory & Items Module

### Структура
- **InventoryService.cs** — хранит предметы, обрабатывает их использование
- **InventoryObject.cs** — объект в сцене, который можно подобрать    
- **ItemSO** — ScriptableObject со свойствами предмета    

### Интеграция
- Разместите `InventoryService` на сцене    
- На предметы навешивайте `InventoryObject`, который имплементирует `IInteractible`    
- Игрок должен взаимодействовать с `InventoryObject` через `IInteractible`

#### Пример:
`InventoryService` предоставляет метод `SetUseItemCallback`, позволяющий обработать использование предмета из инвентаря.

```cs
private void OnEnable()
{
    inventoryService.SetUseItemCallback(OnItemUsed);
}

private void OnDisable()
{
    inventoryService.SetUseItemCallback(null);
}

private void OnItemUsed(ItemSO item)
{
    Debug.Log($"Игрок использовал: {item.itemName}");
}
```

> 💡 Используйте это для создания контекста, например, открытия двери нужным ключом или активации механизма.