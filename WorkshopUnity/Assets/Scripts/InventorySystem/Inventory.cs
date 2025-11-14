using System.Collections.Generic;

namespace WU.InventorySystem
{
    public class Inventory
    {
        public IReadOnlyDictionary<InventoryItemData, int> Items => items;
        
        public static readonly Inventory Instance = new Inventory(); 
        private Dictionary<InventoryItemData, int> items;
        private Inventory()
        {
            items = new Dictionary<InventoryItemData, int>();
        }

        public void AddOrRemoveItem(InventoryItemData data, int quantity = 1)
        {
            if (!items.TryAdd(data, quantity))
                items[data] += quantity;

            if (items[data] <= 0)
                items.Remove(data);
        }

        public int GetItemQuantity(InventoryItemData data)
        {
            return items.GetValueOrDefault(data, 0);
        }
    }
}