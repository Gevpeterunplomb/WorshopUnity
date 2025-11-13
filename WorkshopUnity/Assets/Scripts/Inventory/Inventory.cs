using System.Collections.Generic;
using WU.Inventory.Data;

namespace WU.Inventory
{
    public class Inventory
    {
        public static readonly Inventory Instance = new Inventory(); 

        private Dictionary<InventoryItemData, int> items;

        private Inventory()
        {
            items = new Dictionary<InventoryItemData, int>();
        }

        public void AddOrRemoveItem(InventoryItemData data, int quantity = 1)
        {
            if (items.ContainsKey(data))
                items[data] += quantity;
            else
                items.Add(data, quantity);

            if (items[data] <= 0)
                items.Remove(data);
        }

        public int GetItemQuantity(InventoryItemData data)
        {
            if(items.ContainsKey(data))
                return items[data];
            
            return 0;
        }

        public IReadOnlyDictionary<InventoryItemData, int> Items => items;
    }
}