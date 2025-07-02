using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Interactibles;
using Minigames;
using Inventory;
using Menu;

namespace UI
{
    public class MinigameStartTrigger : MonoBehaviour, IInteractible
    {
        /*
        [SerializeField]
        private List<ItemSO> requiredItems = new List<ItemSO>();

        private List<ItemSO> _insertedItems;
        */
        
        [SerializeField]
        private MinigameObject minigameObject;

        //private InventoryUiController inventoryUi;
        
        private InventoryService inventoryService;

        [Inject]
        private void Construct(InventoryService service)
        {
            inventoryService = service;
            //inventoryUi = FindFirstObjectByType<InventoryUiController>();
        }

        [Button]
        public void Interact()
        {
            minigameObject.Load();
            /*
            if (requiredItems.Count == 0)
                minigameObject.Load();
            else
                inventoryUi.Open(OnItemUse); Example of opening UI with method pass, arg: Action<ItemSO>
            */
        }

        /*
        public void OnItemUse(ItemSO item)
        {
            if (requiredItems.Remove(item))
            {
                inventoryService.RemoveItem(item);
                inventoryUi.Close();
            }
            else
            {
                inventoryUi.Close();
            }
        }
        */
    }
}