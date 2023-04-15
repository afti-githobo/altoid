using UnityEngine;
using UnityEngine.UI;

namespace Altoid.Battle.Frontend
{
    public class BattlerHudEntropyBar : MonoBehaviour
    {
        private Image image;
        [SerializeField]
        private Sprite[] SpriteTable;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        internal void MinusFromTo(int entropy, int newEntropy)
        {
            image.sprite = SpriteTable[newEntropy];
            // do an animation
        }

        internal void PlusFromTo(int entropy, int newEntropy)
        {
            image.sprite = SpriteTable[newEntropy];
            // do an animation
        }
    }
}