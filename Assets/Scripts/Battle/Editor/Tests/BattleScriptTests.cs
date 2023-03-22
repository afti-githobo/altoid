using NUnit.Framework;
using UnityEngine;
using Altoid.Battle.Logic;
using System;

public class BattleScriptTests
{
    const string scriptName = "test.bscript";

    [Test]
    public void BattleScriptParsesAsExpected()
    {
        const string script = "///Comment\n#Label0\nPushFloat 6 4 2 0 \nIntAdd 1 2\n///Comment\nIntSub 4\n#Label1\nIntMult 111";
        var parsed = BattleScript.Parse(script, scriptName);
        int[] expectedCode = new int[]{
            (int)BattleScriptCmd.PushFloat, 4,
            BitConverter.ToInt32(BitConverter.GetBytes(6f)), BitConverter.ToInt32(BitConverter.GetBytes(4f)), BitConverter.ToInt32(BitConverter.GetBytes(2f)), BitConverter.ToInt32(BitConverter.GetBytes(0f)),
            (int)BattleScriptCmd.PushInt, 2,
            1, 2,
            (int)BattleScriptCmd.IntAdd,
            (int)BattleScriptCmd.PushInt, 1,
            4,
            (int)BattleScriptCmd.IntSub,
            (int)BattleScriptCmd.PushInt, 1,
            111,
            (int)BattleScriptCmd.IntMult
        };
        Assert.AreEqual(expectedCode.Length, parsed.Code.Count, $"Disassembly:\n\n\n{BattleScript.Disassemble(parsed.Code)}");
        for (int i = 0; i < parsed.Code.Count; i++)
        {
            Assert.AreEqual(expectedCode[i], parsed.Code[i], $"At address {i}\nDisassembly:\n\n\n{BattleScript.Disassemble(parsed.Code)}");
        }
        Assert.AreEqual(2, parsed.Labels.Count);
        Assert.AreEqual(0, parsed.Labels["Label0"]);
        Assert.AreEqual(15, parsed.Labels["Label1"]);
        Debug.Log($"Disassembly:\n{BattleScript.Disassemble(parsed.Code)}");
    }
}