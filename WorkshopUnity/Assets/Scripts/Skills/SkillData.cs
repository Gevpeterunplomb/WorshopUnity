using UnityEngine;

namespace WU.Skills
{
    public abstract class SkillData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        
        [field: SerializeField, Min(0)]
        public int SpiritForceCost { get; private set; }
        [field: SerializeField, Min(0)]
        public int OverchargeGain { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }


        public abstract ISkill Convert();
    }
}