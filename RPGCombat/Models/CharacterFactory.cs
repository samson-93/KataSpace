namespace RPGCombat.Models;

public class CharacterFactory()
{
    public Character Create(CharacterType type)
    {
        return type switch
        {
            CharacterType.Melee => new Character()
            {
                Class = CharacterType.Melee,
                AttackRange = 2,
            },
            CharacterType.Ranged => new Character()
            {
                Class = CharacterType.Ranged,
                AttackRange = 20,
            },
            _ => throw new ArgumentException("Invalid character type")
        };
    }
}