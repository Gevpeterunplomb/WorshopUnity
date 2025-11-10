using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WU.Monsters;

namespace WU.Level
{
    public class BattleManager : MonoBehaviour
    {
        [field: SerializeField]
        public MonsterData[] MonstersData { get; private set; }
        
        [field: SerializeField, Range(0, 100)]
        public int SpiritForceRegeneration { get; private set; }
        
        public int MaxOvercharge {get; private set;}
        
        public int CurrentOvercharge {get; private set;}
        
        public Monster[] Monsters { get; private set; }
        
        [field: SerializeField]
        private Transform EnemySpawnPoint;
        
        [field: SerializeField]
        private Transform PlayerSpawnPoint;
        public bool IsDoingBattle { get; private set; }

        private void Start()
        {
            Begin();
        }

        public void Begin()
        {
            Debug.Log("Battle Begin");
            
            Monsters = new Monster[MonstersData.Length];
            for (int i = 0; i < MonstersData.Length; i++)
            {
                MonsterData monsterData = MonstersData[i];
                GameObject instance = monsterData.Prefab.TryGetComponent(out PlayerMonster playerMonster) ? 
                    Instantiate(monsterData.Prefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation):
                    Instantiate(monsterData.Prefab, EnemySpawnPoint.position, EnemySpawnPoint.rotation);

                if (instance.TryGetComponent(out Monster monster))
                {
                    monster.Initialize(this, monsterData);
                    Monsters[i] = monster;
                }
            }
        }

        public void Execute()
        { 
            Debug.Log("Battle Execute");
            StartCoroutine(BattleRoutine());
        }

        public void End()
        {
            Debug.Log("Battle end");
        }

        private IEnumerator BattleRoutine()
        {
            IsDoingBattle = true;

            List<Monster> sortedMonster = new(Monsters);
            
            sortedMonster.Sort(CompareMonsterSpeed);
            sortedMonster.Reverse();
            
            while (IsAnyMonsterDead() == false)
            {
                foreach (var monsterTurn in sortedMonster)
                {
                    monsterTurn.BeginTurn();

                    yield return new WaitUntil(monsterTurn.IsTurnDone);

                    monsterTurn.EndTurn();
                    
                    
                }
            }
            
            IsDoingBattle = false;
        }

        private int CompareMonsterSpeed(Monster a, Monster b)
        {
            return a.CurrentSpeed.CompareTo(b.CurrentSpeed);
        }

        private bool IsAnyMonsterDead()
        {
            for (int i = 0; i < Monsters.Length; i++)
            {
                Monster monster = Monsters[i];
                if (monster.IsDead)
                    return true;
            }
            
            return false;
        }

        public List<Monster> GetEnemyTeamMonsters()
        {
            List<Monster> teamMonsters = new List<Monster>();
            
            for (int i = 0; i < Monsters.Length; i++)
            {
                Monster monster = Monsters[i];
                if(monster is EnemyMonster)
                    teamMonsters.Add(monster);
            }
            
            return teamMonsters;
        }

        public List<Monster> GetPlayerTeamMonsters()
        {
            List<Monster> teamMonsters = new List<Monster>();
            
            for (int i = 0; i < Monsters.Length; i++)
            {
                Monster monster = Monsters[i];
                if(monster is PlayerMonster)
                    teamMonsters.Add(monster);
            }
            
            return teamMonsters;
        }

        public List<Monster> GetSameTeamMonsters(Monster from)
        {
            if (from is PlayerMonster)
                return GetPlayerTeamMonsters();
            else
                return GetEnemyTeamMonsters();
        }
        
        public List<Monster> GetOtherTeamMonsters(Monster from)
        {
            if (from is PlayerMonster)
                return GetEnemyTeamMonsters();
            else
                return GetPlayerTeamMonsters();
        }
    }
}