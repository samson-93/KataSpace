namespace RPGCombat.Models;

public abstract class Entity<T> : IEntity
{
    public int Health { get; set; }
    public bool IsAlive => (Health > 0);
    public Coordinate PositionCoordinate { get; set; } = new Coordinate(0, 0);
    
    public abstract T SetHealth(int health);
    public abstract T SetPosition(int x, int y);
    
    protected static void DecreaseHealth(IEntity entity, int damage)
    {
        entity.Health -= damage;
        if(entity.Health <= 0)
        {
            entity.Health = 0;
        }
    }
    protected static void IncreaseHealth(IEntity entity, int heal)
    {
        if (!entity.IsAlive)
        {
            return;
        }
        
        entity.Health += heal;
    }
}