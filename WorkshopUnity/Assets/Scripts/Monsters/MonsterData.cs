using UnityEngine;
using WU.Skills;

namespace WU.Monsters
{
    [CreateAssetMenu(menuName = "WU/Monster")]
    public class MonsterData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set;}
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public int MaxHealth { get; private set; }
        
        [field: SerializeField]
        public int MaxSpiritForce { get; private set; }
        
        [field: SerializeField]
        public int Strength { get; private set; }
        
        [field: SerializeField]
        public int Defence { get; private set; }
        
        [field: SerializeField]
        public int Speed { get; private set; }
        
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        public SkillData[] Skills { get; private set; }
    }
}