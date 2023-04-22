namespace Altoid.Battle.Types
{
    public enum BattleEventType
    {
        Invalid = -1,
        None = 0,
        Start,
        End,
        EndVictory,
        EndDefeat,
        EscapeAttempt,
        EscapeSuccess,
        EscapeFailed,
        PreTurnActionAny,
        PreTurnActionPlayer,
        PreTurnActionEnemy,
        PostTurnActionAny,
        PostTurnActionPlayer,
        PostTurnActionEnemy
    }
}