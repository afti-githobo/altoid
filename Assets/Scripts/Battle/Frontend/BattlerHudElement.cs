using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Altoid.Battle.Types.Battlers;

namespace Altoid.Battle.Frontend
{
    public class BattlerHudElement : MonoBehaviour
    {
        public Battler Battler { get; private set; }
        public BattlerHudHPBar HPBar { get; private set; }
        // todo - come back to this when we're looking more at persistence
        //public BattlerHudExpBar ExpBar { get; private set; }
        public BattlerHudEntropyBar EntropyBar { get; private set; }

        [SerializeField]
        private Sprite DefaultStanceIcon;

        [SerializeField]
        private Image BattlerMugshotElement;

        [SerializeField]
        private Image BattlerStanceIconElement;

        private int hp = -1;
        //private int exp;
        private int entropy = -1;
        private StanceType stance = StanceType.None;
        private Dictionary<StanceType, Sprite> stanceGlyphTable = new();

        void Awake()
        {
            HPBar = GetComponentInChildren<BattlerHudHPBar>();
            EntropyBar = GetComponentInChildren<BattlerHudEntropyBar>();
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (Battler != null && !Battler.IsHidden)
            {
                UpdateEntropyBarAndStance();
            }
            else gameObject.SetActive(false);
        }

        public void AssignBattler (Battler battler)
        {
            Battler = battler;
            GetMugshot();
            GetStanceGlyphs();
        }

        private void GetMugshot()
        {
            var path = $"{Constants.RESOURCE_PATH_BATTLER_MUGSHOTS}/{Battler.BattlerDef.Identity}";
            var resource = Resources.Load<Sprite>(path);
            if (resource != null) BattlerMugshotElement.sprite = resource;
        }

        private void GetStanceGlyphs()
        {
            var path = $"{Constants.RESOURCE_PATH_STANCE_GLYPHS}/{Battler.BattlerDef.Identity}";
            var resources = Resources.LoadAll<Sprite>(path);
            for (int i = 0; i < resources.Length; i++) stanceGlyphTable.Add((StanceType)Enum.Parse(typeof(StanceType), resources[i].name), resources[i]);
        }

        private void UpdateEntropyBarAndStance()
        {
            var newStance = Battler.Stance;
            var newEntropy = Battler.EntropyLevel;
            if (newStance == stance)
            {
                if (newEntropy > entropy) EntropyBar.MinusFromTo(entropy, newEntropy);
                else if (newEntropy < entropy) EntropyBar.PlusFromTo(entropy, newEntropy);
            }
            else
            {
                var gfx = stanceGlyphTable[newStance];
                BattlerStanceIconElement.sprite = gfx;
            }
            entropy = newEntropy;
        }
    }
}