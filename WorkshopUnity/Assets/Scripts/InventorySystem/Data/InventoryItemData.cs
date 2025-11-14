using UnityEngine;

namespace WU.InventorySystem
{
    public abstract class InventoryItemData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }


        public abstract void Use();
        
    }
}