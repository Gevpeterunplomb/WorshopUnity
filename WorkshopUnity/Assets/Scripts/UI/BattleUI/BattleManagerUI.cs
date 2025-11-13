using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Level;
using WU.Monsters;
using WU.Skills;
using WU.UI.BattleUI.LifePoints;
using WU.UI.BattleUI.MainPanel;
using WU.UI.BattleUI.Mana;
using WU.UI.BattleUI.Skills;

namespace WU.UI.BattleUI
{
    public class BattleManagerUI : MonoBehaviour
    {
        [SerializeField]
        private BattleManager battleManager;

        [SerializeField]
        private MainPanelUI mainPanel;   
        
        [SerializeField]
        private OverchargeUI overchargeUI;

        [SerializeField]
        private SkillPanelUI skillPanel;

        [SerializeField] 
        private ManaUI manaUI;
        
        [SerializeField]
        private HPMonsterUI  hpMonsterUI;

        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private PlayerMonster currentMonster;
        private List<ISkill> skillList = new List<ISkill>();

        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            battleManager.OnMonsterTurnStarts += OnMonsterTurnStarts;
            battleManager.OnMonsterTurnEnds += OnMonsterTurnEnds;
        }

        private void OnDisable()
        {
            battleManager.OnMonsterTurnStarts -= OnMonsterTurnStarts;
            battleManager.OnMonsterTurnEnds -= OnMonsterTurnEnds;
        }
        

        private void OnMonsterTurnStarts(Monster monster)
        {
            if (monster is PlayerMonster playerMonster)
            {
                Show();

                Initialize(playerMonster);
                OpenMainPanel();
            }
            else
            {
                Hide();
            }
        }

        private void OnMonsterTurnEnds(Monster monster)
        {
            if (monster == currentMonster)
            {
                Hide();
            }
        }

        public void Initialize(PlayerMonster playerMonster)
        {
            currentMonster = playerMonster;
            skillPanel.Initialize(this, playerMonster);
            mainPanel.Initialize(this, playerMonster);
            manaUI.Initialize(playerMonster);
            hpMonsterUI.Initialize(playerMonster);
            overchargeUI.Initialize(playerMonster);
            
        }

        public void OpenSkillPanel()
        {
            skillPanel.Open();
            mainPanel.Close();
        }

        public void OpenMainPanel()
        {
            skillPanel.Close();
            mainPanel.Open();
        }

        public void UseSkill(ISkill skill)
        {
            skillList.Add(skill);
            if (battleManager.CurrentOvercharge != 0)
            {
                Debug.Log($"skill list count : {skillList.Count},  current overcharge : {battleManager.CurrentOverchargeUse + 1}");
                if (skillList.Count - 1 != battleManager.CurrentOverchargeUse)
                {
                    return;
                }  
            }
            foreach (var skills in skillList)
            {
                Debug.Log(skills);
                currentMonster.AttackWithSkill(skills);
            }
            skillList.Clear();
            currentMonster.TriggerTurnEnd();
            
            /*
            currentMonster.AttackWithSkill(skill);
            //System pour rejouer a faire plus tard
            currentMonster.TriggerTurnEnd();
            
            for (int i = 0; i < skillList.Count; i++)
            {
                Debug.Log($"skill in list : {skillList[i]}");
                currentMonster.AttackWithSkill(i);
            }
            */ 
        }

        public void UpdateCurrentOverchargeUse()
        {
            var allAmountUse = battleManager.CurrentOverchargeUse 
                            == battleManager.MaxOverchargeUse;
            if (!allAmountUse)
                battleManager.UseOvercharge(overchargeUI.isCompletedOvercharge);
            overchargeUI.Initialize(currentMonster);
            Initialize(BattleManager.instance.currentMonster as  PlayerMonster);
            Debug.Log($"Overcharge Use : {battleManager.CurrentOverchargeUse}");
        }
        

        private void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
