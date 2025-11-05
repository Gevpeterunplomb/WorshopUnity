using System.Collections;
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
        
        public Monster[] Monsters { get; private set; }
        public bool IsDoingBattle { get; private set; }
        
        public void Begin()
        {
            Monsters = new Monster[MonstersData.Length];
            for (int i = 0; i < MonstersData.Length; i++)
            {
                MonsterData monsterData = MonstersData[i];
                GameObject instance = Instantiate(monsterData.Prefab, transform);
                
                if (instance.TryGetComponent(out Monster monster))
                    monster.Initialize(this, monsterData);
            }
            StartCoroutine(BattleRoutine());
        }

        private IEnumerator BattleRoutine()
        {
            IsDoingBattle = true;

            int index = 0;

            while (IsAnyMonsterDead() == false)
            {
                Monster monsterTurn = Monsters[index];
                monsterTurn.BeginTurn();

                yield return new WaitUntil(monsterTurn.IsTurnDone);

                monsterTurn.EndTurn();
                index++;
                if(index >= Monsters.Length)
                    index = 0;
            }
            
            IsDoingBattle = false;
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
    }
}