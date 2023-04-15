namespace Altoid.Battle.Data
{
    [System.Flags]
    public enum BattleFaction
    {
        Invalid = -1,
        None = 0,
        Player = 1,
        GenericEnemy = 1 << 1,
        GenericAlly = 1 << 2,
        GenericThirdParty = 1 << 3
    }

}