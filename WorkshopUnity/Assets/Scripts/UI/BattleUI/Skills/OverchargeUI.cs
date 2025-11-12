using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Monsters;

namespace WU.UI.BattleUI.Skills
{
    public class OverchargeUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text skillName;
        [SerializeField]
        private TMP_Text cost;

        [SerializeField] 
        private Button button;
        
        public void Initialize(PlayerMonster playerMonster)
        {
            skillName.text = "Pas encore";
            button.interactable = false;
        }
    }
}