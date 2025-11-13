using UnityEngine;
using WU.Monsters;

namespace WU.Inventory.Data
{
    [CreateAssetMenu(menuName = "WU/Inventory/Item")]
    public class RedBerry : InventoryItemData
    {
        [field: SerializeField]
        public int HealAmount { get; private set; }
        
        protected override void Use(Monster monster)
        {
            monster.AddOrRemoveHealth(HealAmount);
        }
    }
}