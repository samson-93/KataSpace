namespace RPGCombat.Models;

public class Thing(string name) : Entity<Thing>
{
    public string Name => name;
    public override Thing SetHealth(int health)
    {
        this.Health = health;
        
        return this;
    }
    public override Thing SetPosition(int x, int y)
    {
        this.PositionCoordinate.X = x;
        this.PositionCoordinate.Y = y;
        
        return this;
    }
}