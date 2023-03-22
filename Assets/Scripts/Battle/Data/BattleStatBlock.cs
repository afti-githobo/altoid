namespace Altoid.Battle.Data
{
    public readonly struct BattleStatBlock
    {
        public readonly int MaxHP;
        public readonly int Attack;
        public readonly int Defense;
        public readonly int Dexterity;
        public readonly int Agility;
        public readonly int Speed;

        public BattleStatBlock(int maxHP, int attack, int defense, int dexterity, int agility, int speed)
        {
            MaxHP = maxHP;
            Attack = attack;
            Defense = defense;
            Dexterity = dexterity;
            Agility = agility;
            Speed = speed;
        }
    }
}