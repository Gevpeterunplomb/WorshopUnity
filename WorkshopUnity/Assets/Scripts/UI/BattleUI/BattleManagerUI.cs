using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Level;
using WU.Monsters;
using WU.Skills;
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
        private SkillPanelUI skillPanel;

        [SerializeField] 
        private ManaUI manaUI;

        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private PlayerMonster currentMonster;

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
            currentMonster.AttackWithSkill(skill);
            
            //System pour rejouer a faire plus tard
            currentMonster.TriggerTurnEnd();
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
