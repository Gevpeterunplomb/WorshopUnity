using System.Collections.Generic;
using UnityEngine;
using WU.Monsters;

namespace WU.Skills.MaskThrow
{
    public class MaskThrow : Skill<MaskThrowData>
    {
        public MaskThrow(Monster skillOwner, MaskThrowData data) : base(skillOwner, data)
        {
            
        }

        public override void UseAgainst(List<Monster> targets)
        {
            base.UseAgainst(targets);
            
            int realDamage = SkillData.Damage * SkillOwner.CurrentStrength;
            foreach (Monster monster in targets)
                monster.Damage(realDamage);
        }
    }
}