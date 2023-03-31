namespace Altoid.Battle.Logic
{
    public partial class BattleRunner
    {
        [BattleScript(BattleScriptCmd.EndActionPacket)]
        public void Cmd_EndActionPacket()
        {
            currentPacketIndex++;
            currentPacket = null;
        }
    }
}