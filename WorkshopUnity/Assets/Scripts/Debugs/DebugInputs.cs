using UnityEngine;
using WU.InventorySystem;

namespace WU.Debugs
{
    public class DebugInputs : MonoBehaviour
    {
        [SerializeField] private InventoryItemData data;
        [SerializeField] private int amount = 3;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Inventory.Instance.AddOrRemoveItem(data, amount);
            }
        }
    }
}