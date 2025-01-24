namespace HammerTime.Data
{
    public enum SoldierAbilities
    {
        DeepStrike,
        DeadlyDemise,
        FightsFirst,
        FiringDeck,
        Infiltrators,
        Leader,
        LoneOperative,
        Scouts,
        Stealth
    }
}

public static class SoldierAbilitiesExtensions
{
    public static string ToHumanReadable(this SoldierAbilities ability)
    {
        switch (ability)
        {
            case SoldierAbilities.DeepStrike:
                return "Deep Strike";
            case SoldierAbilities.DeadlyDemise:
                return "Deadly Demise";
            case SoldierAbilities.FightsFirst:
                return "Fights First";
            case SoldierAbilities.FiringDeck:
                return "Firing Deck";
            case SoldierAbilities.Infiltrators:
                return "Infiltrators";
            case SoldierAbilities.Leader:
                return "Leader";
            case SoldierAbilities.LoneOperative:
                return "Lone Operative";
            case SoldierAbilities.Scouts:
                return "Scouts";
            case SoldierAbilities.Stealth:
                return "Stealth";
            default:
                return ability.ToString();
        }
    }
}