using System.Collections.Generic;
using WU.Monsters;

namespace WU.Skills
{
    public interface ISkill
    {
        List<Monster> GetTargets();
        void UseAgainst(List<Monster> targets);
    }
}