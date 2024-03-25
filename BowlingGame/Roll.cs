namespace BowlingGame;

public class Roll(int frame, int pins, RollType type = RollType.Default)
{
    public int Value => pins;
    public RollType Type => type;
}

public enum RollType
{
    Default,
    Spare,
    Strike
}