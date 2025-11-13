using UnityEngine;
using WU.Level;
using WU.Monsters;

namespace WU.Inventory.Data
{
    [CreateAssetMenu(menuName = "WU/Inventory/Item")]
    public class RedBerry : InventoryItemData
    {
        [field: SerializeField]
        public int HealAmount { get; private set; }

        public override void Use(BattleManager manager)
        {
            manager.currentMonster.AddOrRemoveHealth(HealAmount);
        }
    }
}