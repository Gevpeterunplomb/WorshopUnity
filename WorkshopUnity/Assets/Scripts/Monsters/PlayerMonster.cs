using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    public class PlayerMonster : Monster
    {
        public event Action<int> skillUse;
        public override void BeginTurn()
        {
            base.BeginTurn();
            Debug.Log("Player turn begin");
            Debug.Log($"Mana before spell : {CurrentSpiritForce}");
            //Index a modifier selon le skill choisit
            AttackWithSkill(0);
            Debug.Log($"Mana after spell : {CurrentSpiritForce}");
            isTurnDone = true;
        }


        public override void EndTurn()
        {
            Debug.Log("Player turn end");
        }

        private void AttackWithSkill(int skillIndex)
        {
            skillUse?.Invoke(skillIndex);
            List<Monster> otherTeam = Skills[skillIndex].GetTargets();
            Skills[skillIndex].UseAgainst(otherTeam);
            
            
        }
        private void Update()
        {
            if(!IsPlaying)
                return;

            ISkill skill = null;
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
                skill = Skills[0];
            if (Input.GetKeyDown(KeyCode.Alpha2))
                skill = Skills[1];
            if (Input.GetKeyDown(KeyCode.Alpha3))
                skill = Skills[2];
            if (Input.GetKeyDown(KeyCode.Alpha4))
                skill = Skills[3];
            */
            if (skill != null)
            {
                List<Monster> targets = skill.GetTargets();
                skill.UseAgainst(targets);
                
                TriggerTurnEnd();
            }

        }

        public void TriggerTurnEnd()
        {
            isTurnDone = true;
        }
    }
}