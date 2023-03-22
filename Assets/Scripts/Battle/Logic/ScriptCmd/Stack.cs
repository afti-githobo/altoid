using System;
using System.Collections.Generic;
using System.Text;

namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        private Stack<int> _stack = new();

        private void PopFloat(out float v) => v = _PopFloat();
        private void PopFloat(out float v0, out float v1)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2, out float v3)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
            v3 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2, out float v3, out float v4)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
            v3 = _PopFloat();
            v4 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2, out float v3, out float v4, out float v5)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
            v3 = _PopFloat();
            v4 = _PopFloat();
            v5 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2, out float v3, out float v4, out float v5, out float v6)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
            v3 = _PopFloat();
            v4 = _PopFloat();
            v5 = _PopFloat();
            v6 = _PopFloat();
        }
        private void PopFloat(out float v0, out float v1, out float v2, out float v3, out float v4, out float v5, out float v6, out float v7)
        {
            v0 = _PopFloat();
            v1 = _PopFloat();
            v2 = _PopFloat();
            v3 = _PopFloat();
            v4 = _PopFloat();
            v5 = _PopFloat();
            v6 = _PopFloat();
            v7 = _PopFloat();
        }

        private void PopInt(out int v) => v = _PopInt();
        private void PopInt(out int v0, out int v1)
        {
            v0 = _PopInt();
            v1 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2, out int v3)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
            v3 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2, out int v3, out int v4)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
            v3 = _PopInt();
            v4 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2, out int v3, out int v4, out int v5)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
            v3 = _PopInt();
            v4 = _PopInt();
            v5 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2, out int v3, out int v4, out int v5, out int v6)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
            v3 = _PopInt();
            v4 = _PopInt();
            v5 = _PopInt();
            v6 = _PopInt();
        }
        private void PopInt(out int v0, out int v1, out int v2, out int v3, out int v4, out int v5, out int v6, out int v7)
        {
            v0 = _PopInt();
            v1 = _PopInt();
            v2 = _PopInt();
            v3 = _PopInt();
            v4 = _PopInt();
            v5 = _PopInt();
            v6 = _PopInt();
            v7 = _PopInt();
        }

        private float _PopFloat() => BitConverter.Int32BitsToSingle(_stack.Pop());

        private int _PopInt() => _stack.Pop();

        private IReadOnlyList<float> _PopFloatArray()
        {
            List<float> ls = new();
            var len = _stack.Pop();
            for (int i = 0; i < len; i++) ls.Add(_PopFloat());
            return ls;
        }

        private IReadOnlyList<int> _PopIntArray()
        {
            List<int> ls = new();
            var len = _stack.Pop();
            for (int i = 0; i < len; i++) ls.Add(_PopInt());
            return ls;
        }

        private void PopFloatArray(out IReadOnlyList<float> vs) => vs = _PopFloatArray();

        private void PopIntArray(out IReadOnlyList<int> vs) => vs = _PopIntArray();

        private void PopString(out string s) => s = _PopString();

        private void PopString(out string v0, out string v1)
        {
            v0 = _PopString();
            v1 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2, out string v3)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
            v3 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2, out string v3, out string v4)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
            v3 = _PopString();
            v4 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2, out string v3, out string v4, out string v5)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
            v3 = _PopString();
            v4 = _PopString();
            v5 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2, out string v3, out string v4, out string v5, out string v6)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
            v3 = _PopString();
            v4 = _PopString();
            v5 = _PopString();
            v6 = _PopString();
        }
        private void PopString(out string v0, out string v1, out string v2, out string v3, out string v4, out string v5, out string v6, out string v7)
        {
            v0 = _PopString();
            v1 = _PopString();
            v2 = _PopString();
            v3 = _PopString();
            v4 = _PopString();
            v5 = _PopString();
            v6 = _PopString();
            v7 = _PopString();
        }

        private string _PopString()
        {
            StringBuilder sb = new();
            var len = _stack.Pop();
            for (int i = 0; i < len; i++) sb.Append(BitConverter.ToChar(BitConverter.GetBytes(_stack.Pop())));
            return sb.ToString();
        }

        private void PushFloat(float v) => _stack.Push(BitConverter.ToInt32(BitConverter.GetBytes(v)));

        private void PushFloat(params float[] vs)
        {
            for (int i = 0; i < vs.Length; i++) PushFloat(vs[i]);
        }

        private void PushInt(int v) => _stack.Push(v);

        private void PushInt(params int[] vs)
        {
            for (int i = 0; i < vs.Length; i++) PushInt(vs[i]);
        }

        private void PushFloatArray(float[] vs)
        {
            Array.Reverse(vs);
            PushFloat(vs);
            _stack.Push(vs.Length);
        }

        private void PushIntArray(int[] vs)
        {
            Array.Reverse(vs);
            PushInt(vs);
            _stack.Push(vs.Length);
        }

        private void PushString(string v)
        {
            for (int i = v.Length - 1; i  > -1; i--) _stack.Push(BitConverter.ToInt32(BitConverter.GetBytes(v[i])));
            _stack.Push(v.Length);
        }

        private int StackDepth { get => _stack.Count; }
        private void ClearStack() => _stack.Clear();

        [BattleScript(BattleScriptCmd.Clear)]
        public void Cmd_Clear() => ClearStack();

        [BattleScript(BattleScriptCmd.PushFloat, typeof(float))]
        public void Cmd_PushFloat()
        {
            var len = currentScript_Next;
            if (len > 1)
            {
                var vs = new float[len];
                for (int i = 0; i < len; i++)
                {
                    vs[i] = BitConverter.ToSingle(BitConverter.GetBytes(currentScript_Next));
                }
                PushFloat(vs);
            }
            else
            {
                PushFloat(currentScript_Next);
            }
        }

        [BattleScript(BattleScriptCmd.PushInt, typeof(int))]
        public void Cmd_PushInt()
        {
            var len = currentScript_Next;
            var vs = new int[len];
            for (int i = 0; i < len; i++)
            {
                vs[i] = currentScript_Next;
            }
            PushInt(vs);
        }

        [BattleScript(BattleScriptCmd.PushString, typeof(string))]
        public void Cmd_PushString()
        {
            var len = currentScript_Next;
            var vs = new char[len];
            for (int i = 0; i < len; i++)
            {
                vs[i] = BitConverter.ToChar(BitConverter.GetBytes(currentScript_Next));
            }
            PushString(new string(vs));
        }

        [BattleScript(BattleScriptCmd.PushFloatArray, typeof(float))]
        public void Cmd_PushFloatArray()
        {
            var len = currentScript_Next;
            var vs = new float[len];
            for (int i = 0; i < len; i++)
            {
                vs[i] = BitConverter.ToSingle(BitConverter.GetBytes(currentScript_Next));
            }
            PushFloatArray(vs);
        }

        [BattleScript(BattleScriptCmd.PushIntArray, typeof(float))]
        public void Cmd_PushIntArray()
        {
            var len = currentScript_Next;
            var vs = new float[len];
            for (int i = 0; i < len; i++)
            {
                vs[i] = BitConverter.ToSingle(BitConverter.GetBytes(currentScript_Next));
            }
            PushFloatArray(vs);
        }
    }
}