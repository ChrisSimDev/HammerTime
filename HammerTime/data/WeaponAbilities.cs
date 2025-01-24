public enum WeaponAbilities
{
    Assault,
    Blast,
    Conversion,
    DevastatingWounds,
    ExtraAttacks,
    Hazardous,
    Heavy,
    IndirectFire,
    IgnoresCover,
    Lance,
    LethalHits,
    LinkedFire,
    Melta,
    Pistol,
    Precision,
    Psychic,
    RapidFire,
    SustainedHits,
    Torrent
}

public static class WeaponAbilitiesExtensions
{
    public static string ToHumanReadable(this WeaponAbilities ability)
    {
        switch (ability)
        {
            case WeaponAbilities.Assault:
                return "Assault";
            case WeaponAbilities.Blast:
                return "Blast";
            case WeaponAbilities.Conversion:
                return "Conversion";
            case WeaponAbilities.DevastatingWounds:
                return "Devastating Wounds";
            case WeaponAbilities.ExtraAttacks:
                return "Extra Attacks";
            case WeaponAbilities.Hazardous:
                return "Hazardous";
            case WeaponAbilities.Heavy:
                return "Heavy";
            case WeaponAbilities.IndirectFire:
                return "Indirect Fire";
            case WeaponAbilities.IgnoresCover:
                return "Ignores Cover";
            case WeaponAbilities.Lance:
                return "Lance";
            case WeaponAbilities.LethalHits:
                return "Lethal Hits";
            case WeaponAbilities.LinkedFire:
                return "Linked Fire";
            case WeaponAbilities.Melta:
                return "Melta";
            case WeaponAbilities.Pistol:
                return "Pistol";
            case WeaponAbilities.Precision:
                return "Precision";
            case WeaponAbilities.Psychic:
                return "Psychic";
            case WeaponAbilities.RapidFire:
                return "Rapid Fire";
            case WeaponAbilities.SustainedHits:
                return "Sustained Hits";
            case WeaponAbilities.Torrent:
                return "Torrent";
            default:
                return ability.ToString();
        }
    }
}