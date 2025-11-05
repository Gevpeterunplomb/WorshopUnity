using System;
using UnityEngine;
using WU.Level;

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
        private BattleManager battleManager;
        public void Initialize(BattleManager manager, MonsterData data)
        {
            battleManager = manager;
            MonsterData = data;
            CurrentHealth = data.MaxHealth;
            CurrentSpiritForce = data.MaxSpiritForce;
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
            AddOrRemoveSpiritForce(battleManager.SpiritForceRegeneration);
        }
        public abstract void EndTurn();
    }
}
