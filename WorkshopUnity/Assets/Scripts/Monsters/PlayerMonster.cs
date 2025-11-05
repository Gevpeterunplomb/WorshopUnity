using UnityEngine;

namespace WU.Monsters
{
    public class PlayerMonster : Monster
    {
        public override void BeginTurn()
        {
            base.BeginTurn();
            Debug.Log("Player turn");
        }

        public override void EndTurn()
        {
            
        }

        public void TriggerTurnEnd()
        {
            isTurnDone = true;
        }
    }
}