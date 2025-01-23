namespace HammerTime.services
{
    public interface IDiceService
    {
        public int[] RollDice(int numberOfDice, int diceFaceCount);
    }
}