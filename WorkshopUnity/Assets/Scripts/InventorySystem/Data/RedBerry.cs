using UnityEngine;
using WU.Level;

namespace WU.InventorySystem
{
    [CreateAssetMenu(menuName = "WU/Inventory/Item")]
    public class RedBerry : InventoryItemData
    {
        [field: SerializeField]
        public int HealAmount { get; private set; }
        
        [field: SerializeField]
        public int RegenSpiritAmount { get; private set; }

        public override void Use()
        {
            BattleManager.instance.currentMonster.AddOrRemoveHealth(HealAmount);
            BattleManager.instance.currentMonster.AddOrRemoveSpiritForce(RegenSpiritAmount);
        }
    }
}