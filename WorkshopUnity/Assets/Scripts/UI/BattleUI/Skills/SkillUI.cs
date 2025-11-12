using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Monsters;
using WU.Skills;

namespace WU.UI.BattleUI.Skills
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text skillName;
        [SerializeField]
        private TMP_Text cost;

        public ISkill CurrentSkill { get; private set; }
        
        [SerializeField]
        private Button button;

        private SkillPanelUI skillPanel;
        
        public void Initialize(SkillPanelUI skillPanelUI, PlayerMonster monster, ISkill skill)
        {
            skillPanel = skillPanelUI;
            CurrentSkill = skill;

            bool hasEnoughForce = monster.CurrentSpiritForce >= skill.Data.SpiritForceCost;
            button.interactable = hasEnoughForce;
            
            skillName.text = CurrentSkill.Data.Name;
            cost.text = CurrentSkill.Data.SpiritForceCost.ToString();
        }
        
        public void OnClicked()
        {
            skillPanel.UseSkill(this);
        }
    }
}