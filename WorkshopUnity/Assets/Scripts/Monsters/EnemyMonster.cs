using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    public class EnemyMonster : Monster
    {
        public override void BeginTurn()
        {
            base.BeginTurn();
            StartCoroutine(EnemyTurnRoutine());
        }

        private IEnumerator EnemyTurnRoutine()
        {
            Debug.Log("Enemy starting turn...");
            yield return new WaitForSeconds(2);
            
            int rand = UnityEngine.Random.Range(0, Skills.Length);
            ISkill skill = Skills[rand];

            List<Monster> targets = skill.GetTargets();
            skill.UseAgainst(targets);

            Debug.Log($"Enemy chose skill {skill.Data.name}");
            yield return new WaitForSeconds(2);

            isTurnDone = true;
            
            Debug.Log("Enemy ending turn...");
        }
    }
}