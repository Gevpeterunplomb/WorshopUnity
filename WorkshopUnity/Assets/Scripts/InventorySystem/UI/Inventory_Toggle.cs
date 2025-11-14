using Helteix.ChanneledProperties.Priorities;
using UnityEngine;
using WU.Player;

namespace WU.InventorySystem
{
    public class InventoryToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CanvasGroup inventoryCanvasGroup;        // UI inventaire
        [SerializeField] private GameObject crosshairManager;              // UI viseur
        [SerializeField] private player_controller playerController;       // script joueur
        [SerializeField] private CanvasGroup cursorCanvasGroup;            // UI curseur

        [field: SerializeField]
        public InventoryUI InventoryUI { get; private set; }
    
        [Header("Settings")]
        [SerializeField] private KeyCode toggleKey = KeyCode.E;

        private bool currentState;

        private void OnEnable()
        {
            GameController.CursorLockModePriority.AddPriority(this, PriorityTags.Default, CursorLockMode.Locked);
            GameController.CursorVisibleStatePriority.AddPriority(this, PriorityTags.Default, false);
        }

        private void OnDisable()
        {
            GameController.CursorLockModePriority.RemovePriority(this);
            GameController.CursorVisibleStatePriority.RemovePriority(this);
        }

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
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                SwitchInventoryVisibility(!currentState);
            }
        }

        public void SwitchInventoryVisibility(bool state)
        {
            if (inventoryCanvasGroup == null)
                return;
            
            InventoryUI.Refresh();
            
            //Inventory
            inventoryCanvasGroup.alpha = state ? 1f : 0f;
            inventoryCanvasGroup.interactable = state;
            inventoryCanvasGroup.blocksRaycasts = state;

            if (crosshairManager != null)
                crosshairManager.SetActive(!state);

            if (playerController != null)
                playerController.canMove = !state;
            
            Time.timeScale = !state ? 1 : 0f;

            // Verrouille la souris
            CursorLockMode lockMode = !state ? CursorLockMode.Locked : CursorLockMode.None;
            bool visible = state;

            GameController.CursorLockModePriority.Write(this, lockMode);
            GameController.CursorVisibleStatePriority.Write(this, visible);
            
            if (cursorCanvasGroup != null)
            {
                cursorCanvasGroup.alpha = state ? 0f : 1f;
                cursorCanvasGroup.interactable = !state;
                cursorCanvasGroup.blocksRaycasts = !state;
            }
            
            currentState = state;
        }
    }
}
