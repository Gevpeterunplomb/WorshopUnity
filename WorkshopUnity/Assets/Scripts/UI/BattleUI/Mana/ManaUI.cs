using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Monsters;

namespace WU.UI.BattleUI
{
    public class ManaUI : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private TMP_Text manaText;

        public void Initialize(PlayerMonster playerMonster)
        {
            manaText.text = playerMonster.CurrentSpiritForce.ToString();
            fill.fillAmount = (float)playerMonster.CurrentSpiritForce / playerMonster.MaxSpiritForce;
            
            
        }
    }
}
