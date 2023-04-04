using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Altoid.Battle.Logic
{
    public abstract class ActionSource
    {
        public ActionSource(Battler b) => Battler = b;

        public readonly Battler Battler;
        public abstract IReadOnlyList<TextAsset> ActionLoadList { get; }

        public abstract Task<BattleAction> SelectNextAction();

        public static ActionSource New(Type t, Battler b) => (ActionSource)t.GetConstructor(new Type[] { typeof(Battler) }).Invoke(new object[] { b });
    }
}