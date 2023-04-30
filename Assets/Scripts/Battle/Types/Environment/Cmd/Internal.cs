namespace Altoid.Battle.Types.Environment
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