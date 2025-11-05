using WU.Monsters;

namespace WU.Skills
{
    public abstract class Skill<T> : ISkill 
        where T : SkillData
    {
        public T SkillData { get; set; }

        protected Skill(T data)
        {
            SkillData = data;
        }

        public abstract void Use(Monster monster);
    }
}