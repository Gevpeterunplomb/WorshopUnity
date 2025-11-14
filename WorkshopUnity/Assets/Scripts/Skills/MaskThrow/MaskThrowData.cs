using UnityEngine;
using WU.Monsters;

namespace WU.Skills
{
    [CreateAssetMenu(menuName = "WU/Skills/MaskThrow")]
    public class MaskThrowData : SkillData
    {
        [field: SerializeField]
        public int Damage { get; private set; }
        
        public override ISkill Convert(Monster owner)
        {
            return new MaskThrow(owner, this);
        }
    }
}