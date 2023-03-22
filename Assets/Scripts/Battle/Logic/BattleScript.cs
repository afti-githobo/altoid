using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Altoid.Battle.Logic
{
    public class BattleScript
    {
        public readonly IReadOnlyList<int> Code;
        public readonly IReadOnlyDictionary<string, int> Labels;

        public BattleScript(IReadOnlyList<int> code, IReadOnlyDictionary<string, int> labels)
        {
            Code = code;
            Labels = labels;
        }

        public static IReadOnlyDictionary<BattleScriptCmd, Action<BattleRunner>> BattleScriptCmdTable { get => _battleScriptCmdTable; }
        private static Dictionary<BattleScriptCmd, Action<BattleRunner>> _battleScriptCmdTable = new();
        private static Dictionary<BattleScriptCmd, Type> _battleScriptOperandTypeTable = new();

        static BattleScript()
        {
            BuildBattleScriptCmdTable();
        }

        /// <summary>
        /// Disassembles the provided BattleScript code and provides it in human-readable form.
        /// </summary>
        /// <param name="code">Parsed BattleScript code</param>
        /// <returns>String containing disassembled BattleScript</returns>
        public static string Disassemble(IReadOnlyList<int> code)
        {

            StringBuilder raw_sb = new();
            for (int i = 0; i < code.Count; i++) raw_sb.Append(code[i] + " ");
            StringBuilder disasm_sb = new();
            for (int i = 0; i < code.Count; i++)
            {
                var op = (BattleScriptCmd)code[i];
                disasm_sb.Append(op);
                disasm_sb.Append(' ');
                if (op == BattleScriptCmd.PushFloat || op == BattleScriptCmd.PushFloatArray)
                {
                    var len = code[i+=1];
                    for (int v = 0; v < len; v++)
                    {
                        disasm_sb.Append(BitConverter.Int32BitsToSingle(code[i+=1]));
                        disasm_sb.Append(' ');
                    }
                }
                else if (op == BattleScriptCmd.PushInt || op == BattleScriptCmd.PushIntArray)
                {
                    var len = code[i+=1];
                    for (int v = 0; v < len; v++)
                    {
                        disasm_sb.Append(code[i+=1]);
                        disasm_sb.Append(' ');
                    }
                }
                else if (op == BattleScriptCmd.PushString)
                {
                    var len = code[i+=1];
                    for (int v = 0; v < len; v++)
                    {
                        disasm_sb.Append(BitConverter.ToChar(BitConverter.GetBytes(code[i+=1])));
                    }
                }
                disasm_sb.AppendLine();
            }
            return raw_sb.ToString() + "\n" + disasm_sb.ToString();
        }

        private static void BuildBattleScriptCmdTable()
        {
            var methods = Assembly.GetAssembly(typeof(BattleRunner)).GetType(typeof(BattleRunner).FullName)
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.GetCustomAttributes(typeof(BattleScriptAttribute), false).Length > 0)
                .ToArray();
            foreach (var method in methods)
            {
                var id = method.GetCustomAttribute<BattleScriptAttribute>().ID;
                if (_battleScriptCmdTable.ContainsKey(method.GetCustomAttribute<BattleScriptAttribute>().ID))
                {
                    throw new Exception($"Unable to build battle script command table - multiple script commands using ID {id}. Script command IDs must be unique.");
                }
                var attr = method.GetCustomAttribute<BattleScriptAttribute>();
                if (attr.OperandType != null && attr.OperandType != typeof(float) && attr.OperandType != typeof(int) && attr.OperandType != typeof(string))
                {
                    throw new Exception($"Unable to build battle script command table - command {id} uses illegal operand type {attr.OperandType}. " +
                        $"Operand types must be either float, int, or string, or otherwise the field should be left null in the attribute constructor.");
                }
                _battleScriptCmdTable[attr.ID] = (runner) => { method.Invoke(runner, new object[0]); };
                _battleScriptOperandTypeTable[attr.ID] = attr.OperandType;
            }
        }

        public static BattleScript Parse(TextAsset script) => Parse(script.text, script.name);

        public static BattleScript Parse (string scriptData, string scriptName)
        {
            var lines = Regex.Split(scriptData, "\r\n|\r|\n");
            var cmdList = new List<int>();
            var labelTable = new Dictionary<string, int>();
            var cmdIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var data = lines[i].Trim();
                if (data.StartsWith("//"))
                {
                    // this is a comment
                    continue;
                }
                else if (data.StartsWith('#'))
                {
                    // this is a label
                    if (data.Contains(' ')) throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - labels cannot contain whitespace");
                    var label = data.Substring(1);
                    if (labelTable.ContainsKey(label)) throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - duplicate labels {label}");
                    labelTable[label] = cmdIndex;
                }
                else
                {
                    // load commands are a special case
                    var splut = data.Split(' ');
                    BattleScriptCmd cmd;
                    try
                    {
                        cmd = (BattleScriptCmd)Enum.Parse(typeof(BattleScriptCmd), splut[0]);
                    }
                    catch
                    {
                        throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - unrecognized command {splut[0]}");
                    }
                    bool addLate = false;
                    if (_battleScriptOperandTypeTable[cmd] == typeof(float))
                    {
                        cmdList.Add((int)BattleScriptCmd.PushFloat);
                        cmdIndex++;
                        if (cmd != BattleScriptCmd.PushFloat && cmd != BattleScriptCmd.PushFloatArray) addLate = true;
                        else if (splut.Length == 1) throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - load command with no data");
                        if (splut.Length > 1)
                        {
                            cmdList.Add(splut.Length - 1);
                            cmdIndex++;
                            for (int i2 = 1; i2 < splut.Length; i2++)
                            {
                                try
                                {
                                    cmdList.Add(BitConverter.SingleToInt32Bits(float.Parse(splut[i2])));
                                    cmdIndex++;
                                }
                                catch
                                {
                                    throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - bad value {splut[i2]}");
                                }
                            }
                        }
                    }
                    else if (_battleScriptOperandTypeTable[cmd] == typeof(int))
                    {
                        cmdList.Add((int)BattleScriptCmd.PushInt);
                        cmdIndex++;
                        if (cmd != BattleScriptCmd.PushInt && cmd != BattleScriptCmd.PushIntArray) addLate = true;
                        else if (splut.Length == 1) throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - load command with no data");
                        if (splut.Length > 1)
                        {
                            cmdList.Add(splut.Length - 1);
                            cmdIndex++;
                            for (int i2 = 1; i2 < splut.Length; i2++)
                            {
                                try
                                {
                                    cmdList.Add(int.Parse(splut[i2]));
                                    cmdIndex++;
                                }
                                catch
                                {
                                    throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - bad value {splut[i2]}");
                                }
                            }
                        }
                    }
                    else if (_battleScriptOperandTypeTable[cmd] == typeof(string))
                    {
                        cmdList.Add((int)BattleScriptCmd.PushString);
                        cmdIndex++;
                        if (cmd != BattleScriptCmd.PushString) addLate = true;
                        else if (splut.Length == 1) throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - load command with no data");
                        if (splut.Length > 1)
                        {
                            var s = data.Substring(splut[0].Length).TrimStart().ToCharArray();
                            cmdList.Add(s.Length);
                            cmdIndex++;
                            for (int i2 = 0; i2 < s.Length; i2++)
                            {
                                try
                                {
                                    cmdList.Add(s[i2]);
                                    cmdIndex++;
                                }
                                catch
                                {
                                    throw new BattleScriptException($"Parse error on script {scriptName} line {i + 1} - bad value {splut[i2]}");
                                }
                            }
                        }
                    }
                    if (addLate)
                    {
                        cmdList.Add((int)cmd);
                        cmdIndex++;
                    }
                }
            }
            return new BattleScript(cmdList, labelTable);
        }
    }
}