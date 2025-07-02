using Interactibles;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryObject : MonoBehaviour, IInteractible
    {
        [SerializeField] 
        private ItemSO itemReference;

        private InventoryService _inventoryService;
        [Inject]
        private void Construct(InventoryService service)
        {
            _inventoryService = service;
        }
        
        public void Interact()
        {
            if (_inventoryService != null) 
                _inventoryService.AddItem(itemReference);
            else
                Debug.LogWarning("InventoryService not found");
        }
    }
}