using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using WU.Inventory;
using WU.Inventory.Data;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI itemText;

    private string itemName;
    private int quantity = 1;
    private string description;

    private InventoryUI manager;

    public void InitializeSlot(InventoryUI inventoryUI, InventoryItemData data, int qtt)
    {
        manager = inventoryUI;
        itemName = data.Title;
        description = data.Description;
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
        
        if (manager != null)
            manager.ShowDescription(description);
    }
}