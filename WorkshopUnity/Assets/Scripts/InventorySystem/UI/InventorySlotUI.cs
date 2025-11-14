using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace WU.InventorySystem
{
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI itemText;

        private string itemName;
        private int quantity = 1;
        private string description;

        private InventoryUI manager;
        private InventoryItemData itemData;

        public void InitializeSlot(InventoryUI inventoryUI, InventoryItemData data, int qtt)
        {
            manager = inventoryUI;
            itemName = data.Title;
            description = data.Description;
            itemData = data;
            quantity = qtt;
            UpdateText();
        }

        private void UpdateText()
        {
            if (itemText != null)
                itemText.text = quantity >= 1 ? $"{itemName} x{quantity}" : itemName;
        }

        // slot cliqué
        public void OnPointerClick(PointerEventData eventData)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            
            // C'EST PAS BON !!!!! (pas propre) signé Loïs
            if (sceneName == "Exploration")
            {
                if (manager != null)
                {
                    manager.ShowDescription(description);
                }
                
                
            }
            else if (sceneName == "FightScene")
            {
                itemData.Use();
                
                Inventory.Instance.AddOrRemoveItem(itemData, -1);
                // à faire à la main parce que pas d'event
                manager.Refresh();
                
                //Close Inventory after item used
                InventoryToggle toggle = manager.GetComponent<InventoryToggle>();
                if (toggle != null)
                {
                    toggle.SwitchInventoryVisibility(false);
                }
            }
        }
    }
}