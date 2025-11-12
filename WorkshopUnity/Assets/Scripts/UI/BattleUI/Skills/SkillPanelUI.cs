using UnityEngine;
using WU.Monsters;
using WU.Skills;

namespace WU.UI.BattleUI.Skills
{
    public class SkillPanelUI : BattlePanelUI
    {
        [SerializeField]
        private GameObject buttonPrefab;
        [SerializeField]
        private Transform skillButtonsParent;

        [SerializeField] 
        private OverchargeUI overchargeUI;

        private BattleManagerUI manager;
        
        public override void Initialize(BattleManagerUI managerUI, PlayerMonster playerMonster)
        {
            manager = managerUI;
            foreach (Transform t in skillButtonsParent)
                Destroy(t.gameObject);

            for (var i = 0; i < playerMonster.Skills.Length; i++)
            {
                ISkill skill = playerMonster.Skills[i];
                GameObject instance = Instantiate(buttonPrefab, skillButtonsParent);

                if (instance.TryGetComponent(out SkillUI skillUI))
                    skillUI.Initialize(this, playerMonster, skill);
            }
            
            overchargeUI.Initialize(playerMonster);
        }

        public void GoBack()
        {
            manager.OpenMainPanel();
        }

        public void UseSkill(SkillUI skillUI)
        {
            manager.UseSkill(skillUI.CurrentSkill);
        }
    }
}