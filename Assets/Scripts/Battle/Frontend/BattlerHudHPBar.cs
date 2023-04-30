using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Altoid.Battle.Types.Battlers;

namespace Altoid.Battle.Frontend
{
    public class BattlerHudHPBar : MonoBehaviour
    {
        [SerializeField]
        private Image HpBarImage;
        [SerializeField]
        private TextMeshProUGUI HpText;

        private int value;
        private Battler battler;

        internal void Hurt(int dmg)
        {
            value -= dmg;
            HpBarImage.fillAmount = value / battler.Stats[Constants.STAT_MAX_HP];
            HpText.text = value.ToString();
            // do an animation
        }

        internal void Heal(int dmg)
        {
            value += dmg;
            HpBarImage.fillAmount = value / battler.Stats[Constants.STAT_MAX_HP];
            HpText.text = value.ToString();
            // do an animation
        }
    }
}