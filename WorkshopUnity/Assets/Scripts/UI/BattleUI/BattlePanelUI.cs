using UnityEngine;
using WU.Monsters;

namespace WU.UI.BattleUI
{
    public abstract class BattlePanelUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        
        public abstract void Initialize(BattleManagerUI manager, PlayerMonster playerMonster);
        
        public virtual void Open()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public virtual void Close()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}