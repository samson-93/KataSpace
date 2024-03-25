using BowlingGame;

namespace BowlingGameTests;

public class GameTests
{
    [Fact]
    public void Roll_OnePin_ScoresOne()
    {
        // Arrange
        var game = new Game();
        
        // Act
        game.Roll(1);
        
        // Assert
        Assert.Equal(1, game.Score());
    }
    
    [Fact]
    public void PlayAFullGame()
    {
        // Arrange
        var game = new Game();
        
        // Act
        
        // frame 1
        game.Roll(1); // 1
        game.Roll(4); // 5
        
        // frame 2
        game.Roll(4); // 9
        game.Roll(5); // 14
        
        // frame 3
        game.Roll(6); // 20
        game.Roll(4); // spare => 29
        
        // frame 4
        game.Roll(5); // 34
        game.Roll(5); // spare => 49
        
        // frame 5
        game.Roll(10); // strike => 60
        
        // frame 6
        game.Roll(0); // 60
        game.Roll(1); // 61
        
        // frame 7
        game.Roll(7); // 68
        game.Roll(3); // spare => 77
        
        // frame 8
        game.Roll(6); // 83
        game.Roll(4); // spare => 97
        
        // frame 9
        game.Roll(10); // strike -> 117
        
        // frame 10
        game.Roll(2); // 119
        game.Roll(8); // spare => 133
        game.Roll(6); // 139
        
        // Assert
        Assert.Equal(139, game.Score());
    }
    
    [Fact]
    public void PlayAGutterGame()
    {
        // Arrange
        var game = new Game();
        
        // Act
        for (var i = 0; i < 20; i++)
        {
            game.Roll(0);
        }
        
        // Assert
        Assert.Equal(0, game.Score());
    }
}