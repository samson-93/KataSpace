using System.Diagnostics;

namespace BowlingGame;

public class Game() : IGame
{
    private List<Roll> _rolls = new List<Roll>(21);
    private int _currentRoll = 0;
    private int _currentFrame = 1;
    private int _currentFrameRoll = 1;
    private int _currentFrameValue = 0;
    
    public void Roll(int pins)
    {
        if(_currentRoll > 21)
        {
            return;
        }
        
        _rolls.Add(new Roll(_currentFrame, pins, GetRollType(pins)));
        _currentFrameValue += pins;

        UpdateFrame();

        _currentRoll++;
    }

    private RollType GetRollType(int pins)
    {
        if(_currentFrameRoll == 1 && pins == 10)
        {
            return RollType.Strike;
        }
        
        if(_currentFrameRoll == 2 && (_currentFrameValue + pins == 10))
        {
            return RollType.Spare;
        }
        
        if(_currentFrameRoll == 2 && (_currentFrameValue + pins == 20))
        {
            return RollType.Strike;
        }

        if (_currentFrameRoll == 3 && (_currentFrameValue + pins == 20))
        {
            return RollType.Spare;
        }
        
        if (_currentFrameRoll == 3 && pins == 10)
        {
            return RollType.Strike;
        }

        return RollType.Default;
    }
    
    private void UpdateFrame()
    {
        if (_currentFrame == 10)
        {
            if (_currentFrameRoll == 2 && _currentFrameValue < 10 || _currentFrameRoll == 3)
            {
                EndGame();
                return;
            }
            
            TickFrame();
            return;
        }

        if(_currentFrameRoll == 1 && _currentFrameValue == 10)
        {
            NextFrame();
            return;
        }
    
        if (_currentFrameRoll == 2)
        {
            NextFrame();
            return;
        }

        TickFrame();
    }
    
    private void TickFrame()
    {
        _currentFrameRoll++;
    }
    
    private void NextFrame()
    {
        _currentFrame++;
        _currentFrameRoll = 1;
        _currentFrameValue = 0;
    }

    private void EndGame()
    {
        _currentRoll = 21;
    }

    public int Score()
    {
        int score = 0;
        
        for(int i = 0; i < _rolls.Count; i++)
        {
            score += _rolls[i].Value;
            
            switch (_rolls[i].Type)
            {
                case RollType.Spare:
                    score += GetNextRoll(i);
                    break;
                case RollType.Strike:
                    score += GetNextRoll(i) + GetNextRoll(i + 1);
                    break;
                case RollType.Default:
                    break;
            }
        }

        return score;
    }
    
    private int GetNextRoll(int i)
    {
        if((_rolls.Count) < (i + 1))
        {
            return 0;
        }
        
        return _rolls[i + 1].Value;
    }
}