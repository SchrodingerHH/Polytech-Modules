using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemObject", menuName = "Internal/ItemObject", order = 0)]
    public class ItemSO : ScriptableObject
    {
        [PreviewField] //Odin Inspector dependency
        public Sprite sprite;
        public string itemName;
        public string itemDescription;
    }
}