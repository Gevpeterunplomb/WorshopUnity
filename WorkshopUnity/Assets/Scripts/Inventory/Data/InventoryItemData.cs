using UnityEngine;
using WU.Level;
using WU.Monsters;

namespace WU.Inventory.Data
{
    public abstract class InventoryItemData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }


        public abstract void Use(BattleManager manager);
        
    }
}