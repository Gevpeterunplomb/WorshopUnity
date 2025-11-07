using System.Collections.Generic;
using WU.Skills;

namespace WU.Monsters
{
    public class EnemyMonster : Monster
    {
        public override void BeginTurn()
        {
            base.BeginTurn();
            
            int rand = UnityEngine.Random.Range(0, Skills.Length);
            ISkill skill = Skills[rand];

            List<Monster> targets = skill.GetTargets();
            skill.UseAgainst(targets);
            
            isTurnDone = true;
        }

        public override void EndTurn()
        {
            
        }
    }
}