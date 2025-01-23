namespace HammerTime.services
{
    public class DiceService : IDiceService
    {
        private readonly Random _random;

        public DiceService()
        {
            _random = new Random();
        }

        public int[] RollDice(int numberOfDice, int diceFaceCount)
        {
            if (numberOfDice <= 0 || diceFaceCount <= 0)
            {
                throw new ArgumentException("Number of dice and dice face count must be greater than zero.");
            }

            int[] results = new int[numberOfDice];
            for (int i = 0; i < numberOfDice; i++)
            {
                results[i] = _random.Next(1, diceFaceCount + 1);
            }

            return results;
        }
    }
}