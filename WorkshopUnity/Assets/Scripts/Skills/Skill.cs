using System.Collections.Generic;
using WU.Level;
using WU.Monsters;

namespace WU.Skills
{
    public abstract class Skill<T> : ISkill 
        where T : SkillData
    {
        public SkillData Data => SkillData;
        
        public T SkillData { get; set; }
        public Monster SkillOwner { get; private set; }

        protected Skill(Monster skillOwner, T data)
        {
            this.SkillOwner = skillOwner;

            SkillData = data;
        }


        public List<Monster> GetTargets()
        {
            List<Monster> targets = new List<Monster>();
            BattleManager battleManager = SkillOwner.BattleManager;

            SkillTarget dataTargets = SkillData.Targets;
            
            if (dataTargets.HasFlagFast(SkillTarget.Allies))
            {
                List<Monster> allies = battleManager.GetSameTeamMonsters(SkillOwner);
                targets.AddRange(allies);
            }
            
            if (dataTargets.HasFlagFast(SkillTarget.Enemies))
            {
                List<Monster> enemies = battleManager.GetOtherTeamMonsters(SkillOwner);
                targets.AddRange(enemies);
            }
            
            return targets;
        }

        public virtual void UseAgainst(List<Monster> targets)
        {
            SkillOwner.AddOrRemoveSpiritForce(-SkillData.SpiritForceCost);
        }
    }
}