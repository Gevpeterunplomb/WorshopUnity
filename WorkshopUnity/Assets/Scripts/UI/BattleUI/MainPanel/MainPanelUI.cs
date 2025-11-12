using UnityEngine;
using WU.Monsters;

namespace WU.UI.BattleUI.MainPanel
{
    public class MainPanelUI : BattlePanelUI
    {
        private BattleManagerUI managerUI;

        public override void Initialize(BattleManagerUI manager, PlayerMonster playerMonster)
        {
            this.managerUI = manager;
        }

        public void OpenSkillPanel()
        {
            managerUI.OpenSkillPanel();
        }

        public void OpenItemsPanel()
        {
            
        }
        
        public void Leave()
        {
            
        }
    }
}