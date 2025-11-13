using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup inventoryCanvasGroup;        // UI inventaire
    [SerializeField] private GameObject crosshairManager;              // UI viseur
    [SerializeField] private player_controller playerController;       // script joueur
    [SerializeField] private CanvasGroup cursorCanvasGroup;            // UI curseur

    [Header("Settings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.E;

    void Start()
    {
        // Cache l'inventaire au démarrage
        if (inventoryCanvasGroup != null)
        {
            inventoryCanvasGroup.alpha = 0f;
            inventoryCanvasGroup.interactable = false;
            inventoryCanvasGroup.blocksRaycasts = false;
        }

        // Affiche le curseur UI
        if (cursorCanvasGroup != null)
        {
            cursorCanvasGroup.alpha = 1f;
            cursorCanvasGroup.interactable = true;
            cursorCanvasGroup.blocksRaycasts = true;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        if (inventoryCanvasGroup == null)
            return;

        bool isVisible = inventoryCanvasGroup.alpha > 0.5f;

        if (isVisible)
        {
            // Fermer l'inventaire
            inventoryCanvasGroup.alpha = 0f;
            inventoryCanvasGroup.interactable = false;
            inventoryCanvasGroup.blocksRaycasts = false;

            if (crosshairManager != null)
                crosshairManager.SetActive(true);

            if (playerController != null)
                playerController.canMove = true;

            // Verrouille la souris
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cursorCanvasGroup != null)
            {
                cursorCanvasGroup.alpha = 1f;
                cursorCanvasGroup.interactable = true;
                cursorCanvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            // Ouvrir l'inventaire
            inventoryCanvasGroup.alpha = 1f;
            inventoryCanvasGroup.interactable = true;
            inventoryCanvasGroup.blocksRaycasts = true;

            if (crosshairManager != null)
                crosshairManager.SetActive(false);

            if (playerController != null)
                playerController.canMove = false;

            // curseur
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (cursorCanvasGroup != null)
            {
                cursorCanvasGroup.alpha = 0f;
                cursorCanvasGroup.interactable = false;
                cursorCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}
