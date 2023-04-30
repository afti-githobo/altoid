using UnityEngine;

namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two floats off the stack, adds a + b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// b: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatAdd, typeof(float))]
        public void Cmd_FloatAdd()
        {
            PopFloat(out var a, out var b);
            PushFloat(a + b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two floats off the stack, subtracts a - b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// b: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatSub, typeof(float))]
        public void Cmd_FloatSub()
        {
            PopFloat(out var a, out var b);
            PushFloat(a - b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two floats off the stack, multiplies a * b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// b: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatMult, typeof(float))]
        public void Cmd_FloatMult()
        {
            PopFloat(out var a, out var b);
            PushFloat(a * b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two floats off the stack, divides a / b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// b: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatDiv, typeof(float))]
        public void Cmd_FloatDiv()
        {
            PopFloat(out var a, out var b);
            PushFloat(a / b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of floats off the stack, finds the minimum value of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: float array
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatMin, typeof(float))]
        public void Cmd_FloatMin()
        {
            PopFloatArray(out var arr);
            var fs = new float[arr.Count];
            for (int i = 0; i < fs.Length; i++) fs[i] = arr[i];
            PushFloat(Mathf.Min(fs));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of floats off the stack, finds the maximum value of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: float array
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatMax, typeof(float))]
        public void Cmd_FloatMax()
        {
            PopFloatArray(out var arr);
            var fs = new float[arr.Count];
            for (int i = 0; i < fs.Length; i++) fs[i] = arr[i];
            PushFloat(Mathf.Max(fs));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of floats off the stack, finds the mean of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: float array
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatMean, typeof(float))]
        public void Cmd_FloatMean()
        {
            PopFloatArray(out var arr);
            var v = 0f;
            for (int i = 0; i < arr.Count; i++) v += arr[i];
            v /= arr.Count;
            PushFloat(v);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, finds its square root, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatSqrt, typeof(float))]
        public void Cmd_FloatSqrt()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Sqrt(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, finds its absolute value, and pushes the result to the stack.</para>
        /// 
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatAbs, typeof(float))]
        public void Cmd_FloatAbs()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Abs(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, finds its sign, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatSign, typeof(float))]
        public void Cmd_FloatSign()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Sign(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, floors it, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatFloor, typeof(float))]
        public void Cmd_FloatFloor()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Floor(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, ceils it, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatCeil, typeof(float))]
        public void Cmd_FloatCeil()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Ceil(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float, rounds it, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatRound, typeof(float))]
        public void Cmd_FloatRound()
        {
            PopFloat(out var a);
            PushFloat(Mathf.Round(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two floats off the stack, finds a random value between a and b (inclusive), and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// b: float
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.FloatRand, typeof(float))]
        public void Cmd_FloatRand()
        {
            PopFloat(out var a, out var b);
            PushFloat(Random.Range(a, b));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops a float off the stack, casts it to an int, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: float
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.Float2Int, typeof(float))]
        public void Cmd_Float2Int()
        {
            PopFloat(out var a);
            PushInt((int)a);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, adds a + b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntAdd, typeof(int))]
        public void Cmd_IntAdd()
        {
            PopInt(out var a, out var b);
            PushInt(a + b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, subtracts a - b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntSub, typeof(int))]
        public void Cmd_IntSub()
        {
            PopInt(out var a, out var b);
            PushInt(a - b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, multiplies a * b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntMult, typeof(int))]
        public void Cmd_IntMult()
        {
            PopInt(out var a, out var b);
            PushInt(a * b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, divides a / b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntDiv, typeof(int))]
        public void Cmd_IntDiv()
        {
            PopInt(out var a, out var b);
            PushInt(a / b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, gets the modulus of a / b, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntMod, typeof(int))]
        public void Cmd_IntMod()
        {
            PopInt(out var a, out var b);
            PushInt(a % b);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of ints off the stack, finds the minimum value of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: int array
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntMin, typeof(int))]
        public void Cmd_IntMin()
        {
            PopIntArray(out var arr);
            var fs = new int[arr.Count];
            for (int i = 0; i < fs.Length; i++) fs[i] = arr[i];
            PushInt(Mathf.Min(fs));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of ints off the stack, finds the maximum value of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: int array
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntMax, typeof(int))]
        public void Cmd_IntMax()
        {
            PopIntArray(out var arr);
            var fs = new int[arr.Count];
            for (int i = 0; i < fs.Length; i++) fs[i] = arr[i];
            PushInt(Mathf.Max(fs));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an array of ints off the stack, finds the mean of the data, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// arr: int array
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntMean, typeof(int))]
        public void Cmd_IntMean()
        {
            PopIntArray(out var arr);
            var v = 0;
            for (int i = 0; i < arr.Count; i++) v += arr[i];
            v /= arr.Count;
            PushInt(v);
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an int, finds its square root, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.IntSqrt, typeof(int))]
        public void Cmd_IntSqrt()
        {
            PopInt(out var a);
            PushFloat(Mathf.Sqrt(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an int, finds its absolute value, and pushes the result to the stack.</para>
        /// 
        /// <para>Operands:</para>
        /// a: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntAbs, typeof(int))]
        public void Cmd_IntAbs()
        {
            PopInt(out var a);
            PushInt(Mathf.Abs(a));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops two ints off the stack, finds a random value between a (inclusive) and b (exclusive), and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// b: int
        /// </summary>
        /// <returns>int</returns>
        [BattleScript(BattleScriptCmd.IntRand, typeof(int))]
        public void Cmd_IntRand()
        {
            PopInt(out var a, out var b);
            PushInt(Random.Range(a, b));
        }

        /// <summary>
        /// <para>Altoid BattleScript command.</para>
        /// 
        /// <para>Pops an int off the stack, casts it to a float, and pushes the result to the stack.</para>
        ///
        /// <para>Operands:</para>
        /// a: int
        /// </summary>
        /// <returns>float</returns>
        [BattleScript(BattleScriptCmd.Int2Float, typeof(int))]
        public void Cmd_Int2Float()
        {
            PopInt(out var a);
            PushFloat(a);
        }
    }
}