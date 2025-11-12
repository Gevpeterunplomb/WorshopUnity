using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    public class PlayerMonster : Monster
    {
        public event Action<ISkill> OnSkillUsed;
        
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
        
        public void AttackWithSkill(int skillIndex) => AttackWithSkill(Skills[skillIndex]);

        public void AttackWithSkill(ISkill skill)
        {
            List<Monster> otherTeam = skill.GetTargets();
            skill.UseAgainst(otherTeam);
            
            OnSkillUsed?.Invoke(skill);
        }

        public void TriggerTurnEnd()
        {
            isTurnDone = true;
        }
    }
}