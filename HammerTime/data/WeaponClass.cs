namespace HammerTime.Data
{
    public record WeaponClass
    {
        public WeaponEnum name { get; set; } = WeaponEnum.Sword;
        public WeaponType type { get; set; } = WeaponType.melee;
        public int strength { get; set; } = 1.0;
        public int armorPenetration { get; set; } = 0;
        public int range { get; set; } = 1;
        public int damage { get; set; } = 1;

        public WeaponAbilities[] weaponAbilities = [];
    }
}