using TMPro;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class BattleTextBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI Text;

        public void Display(string text)
        {
            // todo: localization? + control codes and shit
            Text.text = text;
        }
    }
}