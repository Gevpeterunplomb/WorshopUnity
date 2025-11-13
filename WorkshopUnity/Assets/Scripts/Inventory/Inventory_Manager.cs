using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform contentParent; 
    [SerializeField] private GameObject inventorySlotPrefab; 
    [SerializeField] private TextMeshProUGUI descriptionText;

    public static InventoryManager Instance; 

    // stockage des slots
    private Dictionary<string, InventorySlotUI> slots = new Dictionary<string, InventorySlotUI>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItemToInventory(string itemName, string itemDescription = "")
    {
        if (inventorySlotPrefab == null || contentParent == null)
        {
            Debug.LogWarning("prefab manquante");
            return;
        }

        // nouveau item ou pas
        if (slots.ContainsKey(itemName))
        {
            slots[itemName].AddOne();
            Debug.Log($"🪣 Stack de {itemName} (x{slots[itemName].quantity})");
        }
        else
        {
            // Crée un nouveau slot
            GameObject newSlot = Instantiate(inventorySlotPrefab, contentParent);
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();

            if (slotUI != null)
            {
                slotUI.InitializeSlot(itemName, itemDescription);
                slots.Add(itemName, slotUI);
            }

            Debug.Log($"Nouveau slot ajouté : {itemName}");
        }
    }

    // Appelée slot cliqué
    public void ShowDescription(string description)
    {
        if (descriptionText != null)
            descriptionText.text = description;
    }
}