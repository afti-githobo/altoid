using NUnit.Framework;
using UnityEngine;
using Altoid.Battle.Logic;
using System;
using System.Reflection;
using UnityEngine.TestTools;
using System.Collections;

public class BattleScriptTests
{
    const string scriptName = "test.bscript";

    private object CallPrivate(BattleRunner runner, string name, Type[] types, params object[] args) => typeof(BattleRunner).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance, null, types, null).Invoke(runner, args);

    [Test]
    public void BattleScriptParsesAsExpected()
    {
        const string script = "///Comment\n#Label0\nPushFloat 6 4 2 0 \nIntAdd 1 2\n///Comment\nIntSub 4\n#Label1\nIntMult 111\nNop";
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
            (int)BattleScriptCmd.IntMult,
            (int)BattleScriptCmd.Nop,
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

    [Test]
    public void StackFloatTest()
    {
        const string script = "PushFloat 0 1 2 3\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner();
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.Step();
        Assert.AreEqual(4, env.StackDepth);
        var v0 = (float)CallPrivate(env, "_PopFloat", new Type[0]);
        var v1 = (float)CallPrivate(env, "_PopFloat", new Type[0]);
        var v2 = (float)CallPrivate(env, "_PopFloat", new Type[0]);
        var v3 = (float)CallPrivate(env, "_PopFloat", new Type[0]);
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual(0f, v0);
        Assert.AreEqual(1f, v1);
        Assert.AreEqual(2f, v2);
        Assert.AreEqual(3f, v3);
    }

    [Test]
    public void StackIntTest()
    {
        const string script = "PushInt 0 1 2 3\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner();
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.Step();
        Assert.AreEqual(4, env.StackDepth);
        var v0 = (int)CallPrivate(env, "_PopInt", new Type[0]);
        var v1 = (int)CallPrivate(env, "_PopInt", new Type[0]);
        var v2 = (int)CallPrivate(env, "_PopInt", new Type[0]);
        var v3 = (int)CallPrivate(env, "_PopInt", new Type[0]);
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual(0, v0);
        Assert.AreEqual(1, v1);
        Assert.AreEqual(2, v2);
        Assert.AreEqual(3, v3);
    }

    [Test]
    public void StackStringTest()
    {
        const string script = "PushString ABCDEFG\nNop";    
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner();
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.Step();
        Assert.AreEqual(8, env.StackDepth);
        var v = (string)CallPrivate(env, "_PopString", new Type[0]);
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual("ABCDEFG", v);
    }
}