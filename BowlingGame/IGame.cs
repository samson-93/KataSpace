namespace BowlingGame;

public interface IGame
{
    void Roll(int pins);
    int Score();
}