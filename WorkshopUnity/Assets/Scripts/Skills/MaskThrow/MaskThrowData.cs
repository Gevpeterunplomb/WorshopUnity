using UnityEngine;

namespace WU.Skills.MaskThrow
{
    public class MaskThrowData : SkillData
    {
        [field: SerializeField]
        public int Damage { get; private set; }


        public override ISkill Convert()
        {
            return new MaskThrow(this);
        }
    }
}