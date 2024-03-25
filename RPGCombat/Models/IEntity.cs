namespace RPGCombat.Models;

public interface IEntity
{
    public int Health { get; set; }
    public bool IsAlive { get; }
    public Coordinate PositionCoordinate { get; set; }
}