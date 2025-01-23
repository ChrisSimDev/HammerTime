namespace HammerTime.Data
{
    public record WeaponClass
    {
        public WeaponEnum Name { get; set; } = WeaponEnum.Sword;
        public int WeaponStrength { get; set; } = 1.0;
        public double Weight { get; set; } = 1.0;
    }
}