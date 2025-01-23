namespace HammerTime.data;

public record BaseSoldierClass
{
    public SoldiersEnum soldierType;
    public int move;
    public int weaponSpeed;
    public int ballisticSkill;
    public int strength;
    public int toughness;
    public int wounds;
    public int attacks;
    public int leadership;
    public int save;
    public bool meeleeCapable;
    public bool rangedCapable;
}