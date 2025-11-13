using UnityEngine;
using TMPro;
using System.Collections.Generic;
using WU.Inventory;
using WU.Inventory.Data;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] 
    private Transform contentParent; 
    [SerializeField]
    private GameObject inventorySlotPrefab; 
    [SerializeField] 
    private TextMeshProUGUI descriptionText;

    public void Open()
    {
        foreach (Transform t in contentParent)
            Destroy(t.gameObject);

        foreach ((InventoryItemData inventoryItemData, int quantity) in Inventory.Instance.Items)
        {
            // Crée un nouveau slot
            GameObject newSlot = Instantiate(inventorySlotPrefab, contentParent);
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
            slotUI.InitializeSlot(this, inventoryItemData, quantity);
        }
    }

    // Appelée slot cliqué
    public void ShowDescription(string description)
    {
        if (descriptionText != null)
            descriptionText.text = description;
    }
}