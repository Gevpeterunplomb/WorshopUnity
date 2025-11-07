using System;
using UnityEngine;
using WU.Level;
using WU.Skills;

namespace WU.Monsters
{
    public abstract class Monster : MonoBehaviour
    {
        public event Action<int> OnHealthChange;
        public event Action<int> OnSpiritForceChange;
        
        public int MaxHealth => MonsterData.MaxHealth;
        
        public int MaxSpiritForce => MonsterData.MaxSpiritForce;
        
        public int CurrentStrength => MonsterData.Strength;
        
        public int CurrentDefense => MonsterData.Defence;
        
        public int CurrentSpeed => MonsterData.Speed;
        
        public MonsterData MonsterData { get; private set; }
        public int CurrentHealth { get; private set; }
        public int CurrentSpiritForce { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        protected bool isTurnDone;
        
        public bool IsPlaying { get; private set; }
        public BattleManager BattleManager  { get; private set; }
        public ISkill[] Skills { get; private set; }
        
        public void Initialize(BattleManager manager, MonsterData data)
        {
            BattleManager = manager;
            MonsterData = data;
            CurrentHealth = data.MaxHealth;
            CurrentSpiritForce = data.MaxSpiritForce;
            Skills = new ISkill[data.Skills.Length];
            for (int i = 0; i < Skills.Length; i++)
            {
                SkillData skillData = data.Skills[i];

                ISkill skill = skillData.Convert(this);
                Skills[i] = skill;
            }
        }

        public void Damage(int amount)
        {
            int realDamage = amount / CurrentDefense;
            AddOrRemoveHealth(-realDamage);
        }

        public void Heal(int amount)
        {
            AddOrRemoveHealth(amount);
        }
        
        public void AddOrRemoveHealth(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MonsterData.MaxHealth) 
                CurrentHealth = MonsterData.MaxHealth;
            if(CurrentHealth < 0)
                CurrentHealth = 0;

            if (CurrentHealth == 0)
                Die();
            
            OnHealthChange?.Invoke(amount);
        }

        public void AddOrRemoveSpiritForce(int amount)
        {
            CurrentSpiritForce += amount;
            if(CurrentSpiritForce > MonsterData.MaxSpiritForce)
                CurrentSpiritForce = MonsterData.MaxSpiritForce;
            if(CurrentSpiritForce < 0)
                CurrentSpiritForce = 0;
            
            OnSpiritForceChange?.Invoke(amount);
        }

        private void Die()
        {
            
        }

        public bool IsTurnDone() => isTurnDone;

        public virtual void BeginTurn()
        {
            isTurnDone = false;
            IsPlaying = true;
            AddOrRemoveSpiritForce(BattleManager.SpiritForceRegeneration);
        }
        public virtual void EndTurn()
        {
            IsPlaying = false;
        }
    }
}
