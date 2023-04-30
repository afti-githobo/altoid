using Altoid.Persistence;
using System.Collections.Generic;

namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        [BattleScript(BattleScriptCmd.GetSaveFloat, typeof(string))]
        public void Cmd_GetSaveFloat()
        {
            PopString(out var key);
            try
            {
                var v = Save.Current.GetFloat(key);
                PushFloat(v);
            } catch (KeyNotFoundException)
            {
                throw new BattleScriptException($"No float with key {key} in save data");
            }
        }

        [BattleScript(BattleScriptCmd.SetSaveFloat, typeof(string))]
        public void Cmd_SetSaveFloat()
        {
            PopString(out var key);
            PopFloat(out var v);
            Save.Current.SetFloat(key, v);
        }

        [BattleScript(BattleScriptCmd.GetSaveInt, typeof(string))]
        public void Cmd_GetSaveInt()
        {
            PopString(out var key);
            try
            {
                var v = Save.Current.GetInt(key);
                PushInt(v);
            }
            catch (KeyNotFoundException)
            {
                throw new BattleScriptException($"No int with key {key} in save data");
            }
        }

        [BattleScript(BattleScriptCmd.SetSaveInt, typeof(string))]
        public void Cmd_SetSaveInt()
        {
            PopString(out var key);
            PopInt(out var v);
            Save.Current.SetInt(key, v);
        }

        [BattleScript(BattleScriptCmd.GetSaveString, typeof(string))]
        public void Cmd_GetSaveString()
        {
            PopString(out var key);
            try
            {
                var v = Save.Current.GetString(key);
                PushString(v);
            }
            catch (KeyNotFoundException)
            {
                throw new BattleScriptException($"No string with key {key} in save data");
            }
        }

        [BattleScript(BattleScriptCmd.SetSaveString, typeof(string))]
        public void Cmd_SetSaveString()
        {
            PopString(out var key);
            PopString(out var v);
            Save.Current.SetString(key, v);
        }
    }
}
