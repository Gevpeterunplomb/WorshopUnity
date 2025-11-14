using UnityEngine;
using WU.InventorySystem;
using WU.Monsters;

namespace WU.UI.BattleUI
{
    public class MainPanelUI : BattlePanelUI
    {
        [SerializeField]
        private GameObject inventoryPrefab;
        [SerializeField]
        private Transform rootCanvas;
        
        private InventoryToggle inventoryToggle;
        private BattleManagerUI managerUI;

        public override void Initialize(BattleManagerUI manager, PlayerMonster playerMonster)
        {
            managerUI = manager;
            GameObject instance = Instantiate(inventoryPrefab, rootCanvas);
            inventoryToggle = instance.GetComponent<InventoryToggle>();
        }

        public void OpenSkillPanel()
        {
            managerUI.OpenSkillPanel();
        }

        public void OpenItemsPanel()
        {
            /*je recupere les valeurs du dictionnaire d'InventoryItemData
            var values = Inventory.Instance.Items.Values;
            */
            inventoryToggle.InventoryUI.Refresh();
            inventoryToggle.SwitchInventoryVisibility(true);
        }
        
        public void Leave()
        {
            managerUI.BattleManager.End();
        }
    }
}