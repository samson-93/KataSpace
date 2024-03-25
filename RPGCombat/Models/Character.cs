namespace RPGCombat.Models;

public class Character : Entity<Character>
{
    private static int MAX_HEALTH = 1000;
    private static int INITIAL_LEVEL = 1;
    
    public int Level { get; set; }
    
    public CharacterType Class { get; set; }
    public double AttackRange { get; set; }
    
    public List<Faction> Factions { get; set; } = new List<Faction>();
    
    public Character()
    {
        Health = MAX_HEALTH;
        Level = INITIAL_LEVEL;
    }
    
    public override Character SetHealth(int health)
    {
        Health = health;
        
        return this;
    }
    
    public override Character SetPosition(int x, int y)
    {
        PositionCoordinate.X = x;
        PositionCoordinate.Y = y;
        
        return this;
    }
    
    public void Attack(IEntity target, int damage)
    {
        if (this == target)
            return;

        if (!IsInRange(target))
            return;

        if (target is Character)
        {
            if (IsAlly((Character)target))
            {
                return;
            }
            
            damage = AttackModifier((Character)target, damage);
        }
        
        DecreaseHealth(target, damage);
    }
    
    private bool IsInRange(IEntity entity)
    {
        var xDelta = (entity.PositionCoordinate.X - PositionCoordinate.X);
        var yDelta = (entity.PositionCoordinate.Y - PositionCoordinate.Y);
        var distance = Math.Sqrt((xDelta * xDelta) + (yDelta * yDelta));

        if (distance > AttackRange)
        {
            return false;
        }

        return true;
    }
    
    private int AttackModifier(Character character, int damage)
    {
        return (Level - character.Level) switch
        {
            >= 5 => (damage + (damage / 2)),
            <= -5 => (damage / 2),
            _ => damage
        };
    }
    
    public void Heal(Character character, int heal)
    {
        if (!IsAlly(character) && this != character)
            return;
        
        IncreaseCharacterHealth(character, heal);
    }
    
    private void IncreaseCharacterHealth(Character character, int heal)
    {
        IncreaseHealth(character, heal);
        if(character.Health > MAX_HEALTH)
        {
            character.Health = MAX_HEALTH;
        }
    }

    public Character JoinFaction(Faction faction)
    {
        faction.AddMember(this);
        Factions.Add(faction);

        return this;
    }
    
    public Character LeaveFaction(Faction faction)
    {
        faction.RemoveMember(this);
        Factions.Remove(faction);

        return this;
    }

    public bool IsAlly(Character character)
    {
        foreach (var faction in Factions)
        {
            if (faction.Members.Contains(character))
            {
                return true;
            }
        }

        return false;
    }
}