using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Altoid.Battle.Frontend
{
    public class BattlerHudHPBar : MonoBehaviour
    {
        [SerializeField]
        private Image HpBarImage;
        [SerializeField]
        private TextMeshProUGUI HpText;

        internal void HurtFromTo(int maxHp, int hp, int newHp)
        {
            HpBarImage.fillAmount = newHp / maxHp;
            HpText.text = $"{newHp} / {maxHp}";
            // do an animation
        }

        internal void HealFromTo(int maxHp, int hp, int newHp)
        {
            HpBarImage.fillAmount = newHp / maxHp;
            HpText.text = $"{newHp} / {maxHp}";
            // do an animation
        }
    }
}