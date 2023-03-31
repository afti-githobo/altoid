using Altoid.Battle.Data;
using System;
using System.Collections.Generic;

namespace Altoid.Battle.Logic.AI
{
    public static partial class Datasets
    {
        private static Dictionary<BattlerIdentity, Tuple<ActionSource, ActionLoadListSource>> _actionSourceTable = new();
    }
}