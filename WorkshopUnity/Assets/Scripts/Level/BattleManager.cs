using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WU.Monsters;
using WU.Skills;

namespace WU.Level
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager instance;
        public event Action<Monster> OnMonsterTurnStarts; 
        public event Action<Monster> OnMonsterTurnEnds;
        
        [field: SerializeField]
        public MonsterData[] MonstersData { get; private set; }
        
        [field: SerializeField, Range(0, 100)]
        public int SpiritForceRegeneration { get; private set; }
        
        [field: SerializeField, Range(0, 10)]
        public int MaxOvercharge { get; private set;}
        
        public int CurrentOvercharge { get; private set;}
        
        [field: SerializeField, Range(0, 2)]
        public int MaxOverchargeUse { get; private set;}

        public int CurrentOverchargeUse { get; private set; } = 0;
        
        public Monster[] Monsters { get; private set; }
        
        [field: SerializeField]
        private Transform EnemySpawnPoint;
        
        [field: SerializeField]
        private Transform PlayerSpawnPoint;
        public bool IsDoingBattle { get; private set; }

        public Monster currentMonster;

        public GameObject currentMonsterObject;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        private IEnumerator Start()
        {
            Begin();

            yield return new WaitForSeconds(1);
            
            Execute();
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
                if (instance.TryGetComponent(out PlayerMonster player))
                {
                    currentMonsterObject = instance;
                }
                
                if (instance.TryGetComponent(out Monster monster))
                {
                    monster.Initialize(this, monsterData);
                    Monsters[i] = monster;
                }
            }
        }

        public void ReplaceMonster(MonsterData monsterData)
        {
            Destroy(currentMonsterObject);
            currentMonsterObject = Instantiate(monsterData.Prefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            
        }

        public void Execute()
        { 
            Debug.Log("Battle Execute");
            StartCoroutine(BattleRoutine());
        }

        public void End()
        {
            SceneManager.LoadScene("SampleScene");
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
                    currentMonster = monsterTurn;
                    OnMonsterTurnStarts?.Invoke(monsterTurn);

                    yield return new WaitUntil(monsterTurn.IsTurnDone);

                    monsterTurn.EndTurn();
                    OnMonsterTurnEnds?.Invoke(monsterTurn);
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

        public void UpdateOvercharge(ISkill skill)
        {
            CurrentOvercharge += skill.Data.OverchargeGain;
            Debug.Log(skill.Data.name);
        }
        
        public void UseOvercharge(bool isCompletedOvercharge)
        {
            if (isCompletedOvercharge)
            {
                CurrentOvercharge = 0;
                CurrentOverchargeUse = 0;
                currentMonster.GetComponent<PlayerMonster>().LevelUp();
                Debug.Log("Je me transforme ROAAAAAR");
            }
            else
            {
                CurrentOverchargeUse++;
                CurrentOvercharge--;
            }
        }
    }
}