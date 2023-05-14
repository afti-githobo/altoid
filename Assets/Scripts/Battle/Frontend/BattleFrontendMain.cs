using Altoid.Battle.Datastores;
using Altoid.Battle.Types.Battlers;
using Altoid.Battle.Types.Environment;
using Altoid.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Battle.Frontend
{
    public class BattleFrontendMain : MonoBehaviour
    {
        public static BattleFrontendMain Instance { get; private set; }
        private List<BattlerPuppet> puppets = new();

        public ActionMenu ActionMenu { get => _actionMenu; }
        [SerializeField]
        private ActionMenu _actionMenu;

        public BattleTextBox BattleTextBox { get => _battleTextBox; }
        [SerializeField]
        private BattleTextBox _battleTextBox;

        public TurnOrderBar TurnOrderBar { get => _turnOrderBar; }
        [SerializeField]
        private TurnOrderBar _turnOrderBar;

        public IReadOnlyList<BattlerHudElement> BattlerHudElements { get => _battlerHudElements; }
        [SerializeField]
        private BattlerHudElement[] _battlerHudElements;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (!isLocked) BattleRunner.Current.RunBattleLogic();
        }

        private int lockCounter = 0;
        public bool isLocked => lockCounter > 0;
        public void Lock() => lockCounter++;

        public void Unlock()
        {
            lockCounter--;
            if (lockCounter < 0) throw new Exception("BattleFrontendMain.Unlock called w/ lock counter of 0");
        }

        public IEnumerable AddPuppetForBattler(Battler b)
        {
            var prefabTable = Datastore.Query<BattlerPuppetPrefabTable>();
            var row = prefabTable.Data[b.BattlerDef.AssetType];
            if (row == null) throw new Exception($"No prefab table entry for asset type {b.BattlerDef.AssetType}");
            var request = Resources.LoadAsync(row.PrefabAssetPath);
            yield return request;
            var puppet = Instantiate(request.asset as GameObject);
            puppet.transform.parent = transform;
            puppet.name = $"[PUPPET] {b.BattlerDef.name}";
            puppets.Add(puppet.GetComponent<BattlerPuppet>());
        }
    }
}
