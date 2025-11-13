using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Level;
using WU.Monsters;

namespace WU.UI.BattleUI.Skills
{
    public class OverchargeUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text overchargeAmount;

        [SerializeField] 
        private Image fill;
        
        [SerializeField]
        private BattleManager battleManager;

        [SerializeField] 
        private Button button;

        public bool isCompletedOvercharge { get; private set; }

        public void Initialize(PlayerMonster playerMonster)
        {
            var battleManager = playerMonster.BattleManager;
            isCompletedOvercharge = 
                battleManager.CurrentOvercharge ==
                battleManager.MaxOvercharge;
            overchargeAmount.text = $"Overcharge : {battleManager.CurrentOvercharge} / {battleManager.MaxOvercharge}";
            fill.fillAmount = (float)battleManager.CurrentOvercharge / battleManager.MaxOvercharge;

            bool hasEnoughOvercharge = battleManager.CurrentOvercharge > 0;
            button.interactable = hasEnoughOvercharge;
        }

        public void Sync()
        {
            overchargeAmount.text = battleManager.CurrentOvercharge.ToString() + "/" + battleManager.MaxOvercharge.ToString();
            fill.fillAmount = (float) battleManager.CurrentOvercharge / battleManager.MaxOvercharge;
        }
    }
}