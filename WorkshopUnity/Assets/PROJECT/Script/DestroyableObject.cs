using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [Header("Destruction Settings")]
    [TextArea]
    public string messageOnDestroy = "Objet détruit !"; 

    public string itemName = "Objet";
    [TextArea]
    public string itemDescription = "Description";

    void OnDestroy()
    {
        // Ajoute l'objet dans l'inventaire
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(itemName, itemDescription);
        }
    }
}