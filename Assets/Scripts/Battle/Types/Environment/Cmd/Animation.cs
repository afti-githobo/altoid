namespace Altoid.Battle.Types.Environment
{
    public partial class BattleRunner
    {
        public bool EnableAnimPlayback { get; private set; } = true;

        [BattleScript(BattleScriptCmd.PlayBattleAnim)]
        public void Cmd_PlayBattleAnim()
        {
            PopString(out var anim);
            if (EnableAnimPlayback)
            {
                BattleAnimPlayer.Instance.Play(anim);
            }       
        }
    }
}