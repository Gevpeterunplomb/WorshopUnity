using System;

namespace WU.Skills
{
    [Flags]
    public enum SkillTarget
    {
        Allies = 1 << 0,
        Enemies = 1 << 1,
    }

    public static class SkillTargetExtensions
    {
        public static bool HasFlagFast(this SkillTarget value, SkillTarget flag)
        {
            return (value & flag) != 0;
        }
    }
}