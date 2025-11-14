using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Monsters;

namespace WU.UI.BattleUI
{
    public class HPMonsterUI :  MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private TMP_Text lpText;

        private Monster monster;
        
        public void Initialize(Monster monster)
        {
            this.monster = monster;
            monster.OnHealthChange += UpdateHealth;
            
            UpdateHealth(monster.CurrentHealth);
        }

        private void UpdateHealth(int currentHealth)
        {
            lpText.text = monster.CurrentHealth.ToString();
            fill.fillAmount = (float)monster.CurrentHealth / monster.MaxHealth;
        }
    }
}