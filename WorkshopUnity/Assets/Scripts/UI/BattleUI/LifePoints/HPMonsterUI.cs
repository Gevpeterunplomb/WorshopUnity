using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Monsters;

namespace WU.UI.BattleUI.LifePoints
{
    public class HPMonsterUI :  MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private TMP_Text lpText;

        public void Initialize(Monster monster)
        {
            lpText.text = monster.CurrentHealth.ToString();
            fill.fillAmount = (float)monster.CurrentHealth / monster.MaxHealth;
        }
    }
}