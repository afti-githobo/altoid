namespace Altoid.Battle
{
    public static class Constants
    {
        public const int LEVEL_MIN = 1;
        public const int LEVEL_MAX = 100;
        public const int STAT_MIN_MAX_HP = 2;
        public const int STAT_MAX_MAX_HP = 99999;
        public const int STAT_MIN_OTHER = 1;
        public const int STAT_MAX_OTHER = 999;

        public const int ARBITRARY_NO = -1;
        public const int INT_TERMINATOR = int.MinValue;
        public const float FLOAT_TERMINATOR = float.NegativeInfinity;
        public const string STR_TERMINATOR = null;

        public const int FALSE = 0;
        public const int TRUE = 1;

        public const int STAT_MAX_HP = 0;
        public const int STAT_ATK = 1;
        public const int STAT_DEF = 2;
        public const int STAT_DEX = 3;
        public const int STAT_AGI = 4;
        public const int STAT_SPD = 5;

        public const int ENTROPY_MIN = 0;
        public const int ENTROPY_MAX = 10;
        public const int ENTROPY_INFINITE = 11;

        public const string END_ACTION_PACKET_SCRIPT = "endActionPacket";
        public const string END_ACTION_PACKET_SCRIPT_FILENAME = "BScript/Battle/System/endActionPacket";

    }
}