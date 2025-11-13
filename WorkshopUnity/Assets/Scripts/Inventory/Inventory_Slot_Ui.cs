using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI itemText;

    [HideInInspector] public string itemName;
    [HideInInspector] public int quantity = 1;
    [HideInInspector] public string description;

    public void InitializeSlot(string name, string desc)
    {
        itemName = name;
        description = desc;
        quantity = 1;
        UpdateText();
    }

    public void AddOne()
    {
        quantity++;
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
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.ShowDescription(description);
    }
}