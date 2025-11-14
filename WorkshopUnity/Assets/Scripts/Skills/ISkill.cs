using System.Collections.Generic;
using WU.Monsters;

namespace WU.Skills
{
    public interface ISkill
    {
        SkillData Data { get; }
        List<Monster> GetTargets();
        void UseAgainst(List<Monster> targets);
    }
}