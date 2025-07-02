using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class InventoryService : MonoBehaviour
    {
        public IReadOnlyList<ItemSO> Items => _items;
        
        private List<ItemSO> _items = new List<ItemSO>();
        
        [SerializeField] private List<ItemSO> itemsToCollect;

        private Action<ItemSO> onUseItemCallback; 

        private void Awake()
        {
            foreach (var itemSo in itemsToCollect)
            {
                AddItem(itemSo);
            }
        }

        public void AddItem(ItemSO item) => _items.Add(item);

        public void RemoveItem(ItemSO item) => _items.Remove(item);

        public void TryUseItem(ItemSO insertItem)
        {
            onUseItemCallback?.Invoke(insertItem);
        }

        public void SetUseItemCallback(Action<ItemSO> callback)
        {
            onUseItemCallback = callback; 
        }
    }
}