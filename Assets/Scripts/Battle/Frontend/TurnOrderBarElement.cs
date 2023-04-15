using UnityEngine;
using UnityEngine.UI;

namespace Altoid.Battle.Frontend
{
    public class TurnOrderBarElement : MonoBehaviour
    {
        [SerializeField]
        private Image pane;

        [SerializeField]
        private Image mugshot;

        public void SetValues(Color color, Sprite sprite)
        {
            pane.color = color;
            mugshot.sprite = sprite;
        }
    }
}