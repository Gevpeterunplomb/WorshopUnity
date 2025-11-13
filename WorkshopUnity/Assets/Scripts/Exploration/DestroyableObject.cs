using UnityEngine;
using WU.Inventory;
using WU.Inventory.Data;

public class DestroyableObject : MonoBehaviour
{
    [Header("Destruction Settings")]
    [TextArea]
    public string messageOnDestroy = "Objet détruit !";

    [SerializeField]
    private InventoryItemData data;
    
    void OnDestroy()
    { 
        if(data)
            Inventory.Instance.AddOrRemoveItem(data, 1);
    }
}