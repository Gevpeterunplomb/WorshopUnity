using System;
using System.Collections.Generic;
using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    public class PlayerMonster : Monster
    {
        public override void BeginTurn()
        {
            base.BeginTurn();
            Debug.Log("Player turn");
        }

        public override void EndTurn()
        {
            
        }

        private void Update()
        {
            if(!IsPlaying)
                return;

            ISkill skill = null;
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
                skill = Skills[0];
            if (Input.GetKeyDown(KeyCode.Alpha2))
                skill = Skills[1];
            if (Input.GetKeyDown(KeyCode.Alpha3))
                skill = Skills[2];
            if (Input.GetKeyDown(KeyCode.Alpha4))
                skill = Skills[3];

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