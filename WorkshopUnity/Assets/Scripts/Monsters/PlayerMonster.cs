using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    public class PlayerMonster : Monster
    {
        public event Action<ISkill> OnSkillUsed;
        
        [SerializeField]
        private MonsterData evolutionData;
        
        public override void BeginTurn()
        {
            base.BeginTurn();
            Debug.Log("Player turn begin");
            isTurnDone = false;
            
        }
        public override void EndTurn()
        {
            Debug.Log("Player turn end");
        }

        public void LevelUp()
        {
            int currentHealth = CurrentHealth;
            Initialize(BattleManager,  evolutionData);
            BattleManager.ReplaceMonster(evolutionData);
            SetHealth(currentHealth);
        }
        public void AttackWithSkill(int skillIndex) => AttackWithSkill(Skills[skillIndex]);
        
        public void AttackWithSkill(ISkill skill)
        {
            List<Monster> otherTeam = skill.GetTargets();
            skill.UseAgainst(otherTeam);
            BattleManager.UpdateOvercharge(skill);
            OnSkillUsed?.Invoke(skill);
        }

        public void TriggerTurnEnd()
        {
            Debug.Log($"current overcharge {BattleManager.CurrentOvercharge}");
            isTurnDone = true;
        }
    }
}