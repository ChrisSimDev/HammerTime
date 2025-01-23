namespace HammerTime.data
{
    public record Project
    {
        public required string Name { get; init; } 
        public Dictionary<int, BaseSoldierClass> SoldierDictionary { get; init; } = new();
        public int CurrSoldierId { get; set; } = -1;
    }
}