using NUnit.Framework;
using UnityEngine;
using Altoid.Battle.Logic;
using System;
using System.Reflection;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using Altoid.Battle;

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
    }

    [Test]
    public void StackFloatTest()
    {
        const string script = "PushFloat 0 1 2 3\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.StepScripts();
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
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.StepScripts();
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
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.StepScripts();
        Assert.AreEqual(8, env.StackDepth);
        var v = (string)CallPrivate(env, "_PopString", new Type[0]);
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual("ABCDEFG", v);
    }

    [Test]
    public void StackFloatArrayTest()
    {
        const string script = "PushFloatArray 0 1 2 3 4 5\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.StepScripts();
        Assert.AreEqual(7, env.StackDepth);
        var vs = (IReadOnlyList<float>)CallPrivate(env, "_PopFloatArray", new Type[0]);
        var expected = new float[] { 0, 1, 2, 3, 4, 5 };
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual(6, vs.Count);
        for (int i = 0; i < vs.Count; i++) Assert.AreEqual(vs[i], expected[i]);
    }

    [Test]
    public void StackIntArrayTest()
    {
        const string script = "PushIntArray 0 1 2 3 4 5\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        env.StepScripts();
        Assert.AreEqual(7, env.StackDepth);
        var vs = (IReadOnlyList<int>)CallPrivate(env, "_PopIntArray", new Type[0]);
        var expected = new int[] { 0, 1, 2, 3, 4, 5 };
        Assert.AreEqual(0, env.StackDepth);
        Assert.AreEqual(6, vs.Count);
        for (int i = 0; i < vs.Count; i++) Assert.AreEqual(vs[i], expected[i]);
    }

    [Test]
    public void FloatComparisonsTest()
    {
        const string script = "FloatGreaterThan 1 0\nFloatLessThan 0 1\nFloatGreaterThanEquals 1 0\nFloatLessThanEquals 0 1\nFloatEquals 1 1\nFloatNotEquals 0 1\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 12; i++) env.StepScripts();
        Assert.AreEqual(6, env.StackDepth);
        for (int i = 0; i < 6; i++)
        {
            var v = (int)CallPrivate(env, "_PopInt", new Type[0]);
            Assert.AreEqual(Constants.TRUE, v);
        }
        Assert.AreEqual(0, env.StackDepth);
    }

    [Test]
    public void IntComparisonsTest()
    {
        const string script = "IntGreaterThan 1 0\nIntLessThan 0 1\nIntGreaterThanEquals 1 0\nIntLessThanEquals 0 1\nIntEquals 1 1\nIntNotEquals 0 1\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 12; i++) env.StepScripts();
        Assert.AreEqual(6, env.StackDepth);
        for (int i = 0; i < 6; i++)
        {
            var v = (int)CallPrivate(env, "_PopInt", new Type[0]);
            Assert.AreEqual(Constants.TRUE, v);
        }
        Assert.AreEqual(0, env.StackDepth);
    }

    [Test]
    public void StringComparisonsTest()
    {
        const string script = "PushString foo\nStringContains foobar\nPushString bar\nStringNotContains foo\nPushString foo\nStringEquals foo\nPushString foo\nStringNotEquals bar\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 12; i++) env.StepScripts();
        Assert.AreEqual(4, env.StackDepth);
        for (int i = 0; i < 4; i++)
        {
            var v = (int)CallPrivate(env, "_PopInt", new Type[0]);
            Assert.AreEqual(Constants.TRUE, v);
        }
        Assert.AreEqual(0, env.StackDepth);
    }

    [Test]
    public void FloatMathTest()
    {
        const string script = "FloatAdd 1 1\nFloatSub 3 1\nFloatMult 1 2\nFloatDiv 4 2\nPushFloatArray 4 3 2\nFloatMin\nPushFloatArray 0 1 2\nFloatMax\nPushFloatArray 0 4\nFloatMean\n" +
            "FloatSqrt 4\nFloatAbs -2\nFloatFloor 2.5\nFloatCeil 1.5\nFloatRound 2.1\nFloat2Int 2\nFloatRand 0 1\n FloatSign -10\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 30; i++) env.StepScripts();
        Assert.AreEqual(15, env.StackDepth);
        Assert.AreEqual(-1, (float)CallPrivate(env, "_PopFloat", new Type[0]));
        var rand = (float)CallPrivate(env, "_PopFloat", new Type[0]);
        Assert.IsTrue(rand >= 0 && rand <= 1);
        Assert.AreEqual(2, (int)CallPrivate(env, "_PopInt", new Type[0]));
        for (int i = 0; i < 12; i++)
        {
            var v = (float)CallPrivate(env, "_PopFloat", new Type[0]);
            Assert.AreEqual(2, v);
        }
        Assert.AreEqual(0, env.StackDepth);
    }

    [Test]
    public void IntMathTest()
    {
        const string script = "IntAdd 1 1\nIntSub 3 1\nIntMult 1 2\nIntDiv 4 2\nIntMod 5 3\nPushIntArray 4 3 2\nIntMin\nPushIntArray 0 1 2\nIntMax\nPushIntArray 0 4\nIntMean\n" +
            "IntAbs -2\nInt2Float 2\nIntRand 0 2\n IntSqrt 4\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 24; i++) env.StepScripts();
        Assert.AreEqual(12, env.StackDepth);
        Assert.AreEqual(2, (float)CallPrivate(env, "_PopFloat", new Type[0]));
        var rand = (int)CallPrivate(env, "_PopInt", new Type[0]);
        Assert.IsTrue(rand > -1 && rand < 2);
        Assert.AreEqual(2, (float)CallPrivate(env, "_PopFloat", new Type[0]));
        for (int i = 0; i < 9; i++)
        {
            var v = (int)CallPrivate(env, "_PopInt", new Type[0]);
            Assert.AreEqual(2, v);
        }
        Assert.AreEqual(0, env.StackDepth);
    }

    [Test]
    public void FlowControlTestUnconditionalJump()
    {
        const string script = "PushInt 1\nJumpUnconditional SKIP\nPushInt 2\n#SKIP\nNop\nPushInt 3\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(2, env.StackDepth);
        Assert.AreEqual(3, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestConditionalJump()
    {
        const string script = "PushInt 1\nJumpConditional SKIP\nPushInt 2\n#SKIP\nNop\nPushInt 3\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(1, env.StackDepth);
        Assert.AreEqual(3, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestUnconditionalBranch()
    {
        const string script = "PushInt 1\nBranchUnconditional SKIP\nPushInt 2\n#SKIP\nNop\nPushInt 3\nReturn\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 8; i++) env.StepScripts();
        Assert.AreEqual(3, env.StackDepth);
        Assert.AreEqual(2, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestConditionalBranch()
    {
        const string script = "PushInt 1\nBranchConditional SKIP\nPushInt 2\n#SKIP\nNop\nPushInt 3\nReturn\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 8; i++) env.StepScripts();
        Assert.AreEqual(2, env.StackDepth);
        Assert.AreEqual(2, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestUnconditionalJumpToScript()
    {
        const string scriptA = "PushInt 1\nJumpToScriptUnconditional b_test.bscript\nPushInt 2";
        const string scriptB = "PushInt 1\nPushInt 3\nNop";
        var parsedA = BattleScript.Parse(scriptA, "a_" + scriptName);
        var parsedB = BattleScript.Parse(scriptB, "b_" + scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsedA, parsedB);
        env.RunScript("a_" + scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(3, env.StackDepth);
        Assert.AreEqual(3, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestConditionalJumpToScript()
    {
        const string scriptA = "PushInt 1\nJumpToScriptConditional b_test.bscript\nPushInt 2";
        const string scriptB = "PushInt 1\nPushInt 3\nNop";
        var parsedA = BattleScript.Parse(scriptA, "a_" + scriptName);
        var parsedB = BattleScript.Parse(scriptB, "b_" + scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsedA, parsedB);
        env.RunScript("a_" + scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(2, env.StackDepth);
        Assert.AreEqual(3, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestUnconditionalBranchToScript()
    {
        const string scriptA = "PushInt 1\nBranchToScriptUnconditional b_test.bscript\nPushInt 42\nNop";
        const string scriptB = "PushInt 1\nPushInt 3\nReturn";
        var parsedA = BattleScript.Parse(scriptA, "a_" + scriptName);
        var parsedB = BattleScript.Parse(scriptB, "b_" + scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsedA, parsedB);
        env.RunScript("a_" + scriptName);
        for (int i = 0; i < 7; i++) env.StepScripts();
        Assert.AreEqual(4, env.StackDepth);
        Assert.AreEqual(42, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void FlowControlTestConditionalBranchToScript()
    {
        const string scriptA = "PushInt 1\nBranchToScriptConditional b_test.bscript\nPushInt 42\nNop";
        const string scriptB = "PushInt 1\nPushInt 3\nReturn";
        var parsedA = BattleScript.Parse(scriptA, "a_" + scriptName);
        var parsedB = BattleScript.Parse(scriptB, "b_" + scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsedA, parsedB);
        env.RunScript("a_" + scriptName);
        for (int i = 0; i < 7; i++) env.StepScripts();
        Assert.AreEqual(3, env.StackDepth);
        Assert.AreEqual(42, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void SaveTestFloats()
    {
        const string script = "PushFloat 3\nSetSaveFloat val\nGetSaveFloat val\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(1, env.StackDepth);
        Assert.AreEqual(3, (float)CallPrivate(env, "_PopFloat", new Type[0]));
    }

    [Test]
    public void SaveTestInts()
    {
        const string script = "PushInt 3\nSetSaveInt val\nGetSaveInt val\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(1, env.StackDepth);
        Assert.AreEqual(3, (int)CallPrivate(env, "_PopInt", new Type[0]));
    }

    [Test]
    public void SaveTestStrings()
    {
        const string script = "PushString Three\nSetSaveString val\nGetSaveString val\nNop";
        var parsed = BattleScript.Parse(script, scriptName);
        var env = new BattleRunner(null);
        env.LoadScripts(parsed);
        env.RunScript(scriptName);
        for (int i = 0; i < 5; i++) env.StepScripts();
        Assert.AreEqual(6, env.StackDepth);
        Assert.AreEqual("Three", (string)CallPrivate(env, "_PopString", new Type[0]));
    }
}