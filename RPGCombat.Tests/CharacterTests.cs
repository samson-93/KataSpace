using RPGCombat.Models;

namespace RPGCombat.Tests;

public class CharacterTests
{
    [Fact]
    public void Character_Has1000Health_WhenCreated()
    {
        // Arrange
        var character = new Character();

        // Assert
        Assert.Equal(1000, character.Health);
    }

    [Fact]
    public void Character_LevelIs1_WhenCreated()
    {
        // Arrange
        var character = new Character();

        // Assert
        Assert.Equal(1, character.Level);
    }

    [Fact]
    public void Character_IsAlive_WhenCreated()
    {
        // Arrange
        var character = new Character();

        // Assert
        Assert.True(character.IsAlive);
    }
    
    [Fact]
    public void Character_HealthIsReduced_WhenAttackedByCharacter()
    {
        // Arrange
        var character = new Character();
        var targetCharacter = new Character();
        var dmgAmt = 150;
        var expectedHealth = targetCharacter.Health - dmgAmt;

        // Act
        character.Attack(targetCharacter, dmgAmt);

        // Assert
        Assert.Equal(expectedHealth, targetCharacter.Health);
    }
    
    [Fact]
    public void Character_IsDead_WhenHealthIsZero()
    {
        // Arrange
        var character = new Character();
        var targetCharacter = new Character();

        // Act
        character.Attack(targetCharacter, targetCharacter.Health);

        // Assert
        Assert.True(!targetCharacter.IsAlive);
    }
    
    [Fact]
    public void Character_HealthIsIncreased_WhenHealedByCharacter()
    {
        // Arrange
        var character = new Character() { Health = 100 };
        
        // Act
        character.Heal(character, 100);
        
        // Assert
        Assert.Equal(200, character.Health);
    }

    [Fact]
    public void Character_CannotBeHealed_WhenDead()
    {
        // Arrange
        int startingHp = 0;
        var character = new Character { Health = startingHp };
        
        // Act
        character.Heal(character, 100);
        
        // Assert
        Assert.Equal(startingHp, character.Health);
        Assert.True(!character.IsAlive);
    }

    [Fact]
    public void Character_CannotDealDamage_WhenTargettingSelf()
    {
        // Arrange
        var startingHealth = 1000;
        var character = new Character() { Health = startingHealth};
        
        // Act
        character.Attack(character, 100);
        
        // Assert
        Assert.Equal(startingHealth, character.Health);
    }
    
    [Fact]
    public void Character_CanDealDamage_WhenTargettingOther()
    {
        // Arrange
        var startingHealth = 1000;
        var character = new Character();
        var targetCharacter = new Character() { Health = startingHealth };
        var dmgAmt = 100;
        
        // Act
        character.Attack(targetCharacter, dmgAmt);
        
        // Assert
        Assert.Equal(startingHealth - dmgAmt, targetCharacter.Health);
    }

    [Fact]
    public void Character_CanHeal_WhenTargettingSelf()
    {
        // Arrange
        var startingHealth = 1;
        var character = new Character() { Health = startingHealth };
        var healAmt = 100;
        
        // Act
        character.Heal(character, healAmt);
        
        // Assert
        Assert.Equal(startingHealth + healAmt, character.Health);
    }

    [Fact]
    public void Character_CannotHeal_WhenTargettingOther()
    {
        // Arrange
        var startingHealth = 1;
        var character = new Character();
        var targetCharacter = new Character() { Health = startingHealth };
        
        // Act
        character.Heal(targetCharacter, 100);
        
        // Assert
        Assert.Equal(startingHealth, targetCharacter.Health);
    }
    
    [Theory]
    [InlineData(1, 5, 100, 100)]
    [InlineData(1, 6, 100, 50)]
    [InlineData(5, 1, 100, 100)]
    [InlineData(6, 1, 100, 150)]
    public void Character_Deals50PercentMoreDamage_WhenTargetLevelIs5OrLessThan(int lvl, int targetLvl, int attemptedDmg, int actualDmg)
    {
        // Arrange
        var startingHealth = 1000;
        var character = new Character() { Level = lvl };
        var targetCharacter = new Character() { Level = targetLvl, Health = startingHealth };
        var dmgAmt = attemptedDmg;
        var expectedHealth = targetCharacter.Health - actualDmg;

        // Act
        character.Attack(targetCharacter, dmgAmt);

        // Assert
        Assert.Equal(expectedHealth, targetCharacter.Health);
    }
    
    [Theory]
    [InlineData(1, 5, 100, 100)]
    [InlineData(1, 6, 100, 50)]
    public void Character_Deals50PercentLessDamage_WhenTargetLevelIs5OrMoreThan(int lvl, int targetLvl, int attemptedDmg, int actualDmg)
    {
        // Arrange
        var startingHealth = 1000;
        var character = new Character() { Level = lvl };
        var targetCharacter = new Character() { Level = targetLvl, Health = startingHealth };
        var dmgAmt = attemptedDmg;
        var expectedHealth = targetCharacter.Health - actualDmg;

        // Act
        character.Attack(targetCharacter, dmgAmt);

        // Assert
        Assert.Equal(expectedHealth, targetCharacter.Health);
    }
    
    [Theory]
    [InlineData(CharacterType.Melee, 2)]
    [InlineData(CharacterType.Ranged, 20)]
    public void Character_HasMaxRangeByType_WhenCreated(CharacterType type, int expectedRange)
    {
        // Arrange
        var characterFactory = new CharacterFactory();
        
        // Act
        var character = characterFactory.Create(type);
        
        // Assert
        Assert.Equal(expectedRange, character.AttackRange);
    }

    [Fact]
    public void Character_CanAttack_WhenInRange()
    {
        // Arrange
        var startingHealth = 1000;
        var dmgAmt = 100;
        var characterFactory = new CharacterFactory();
        var character = characterFactory
            .Create(CharacterType.Melee)
            .SetPosition(0, 0);
        var targetCharacter = characterFactory
            .Create(CharacterType.Melee)
            .SetHealth(startingHealth)
            .SetPosition(1, 1); 
        
        // Act
        character.Attack(targetCharacter, dmgAmt);
        
        // Assert
        Assert.Equal(startingHealth - dmgAmt, targetCharacter.Health);
    }
    
    [Fact]
    public void Character_CannotAttack_WhenOutOfRange()
    {
        // Arrange
        var startingHealth = 1000;
        var characterFactory = new CharacterFactory();
        var character = characterFactory
            .Create(CharacterType.Melee)
            .SetPosition(0, 0);
        var targetCharacter = characterFactory
            .Create(CharacterType.Melee)
            .SetHealth(startingHealth)
            .SetPosition(3, 3); 
        
        // Act
        character.Attack(targetCharacter, 100);
        
        // Assert
        Assert.Equal(startingHealth, targetCharacter.Health);
    }

    [Fact]
    public void Character_IsFactionless_WhenCreated()
    {
        // Arrange
        var character = new Character();
        
        // Assert
        Assert.Empty(character.Factions);
    }
    
    [Fact]
    public void Character_CanJoinFaction_WhenHasNoFactions()
    {
        // Arrange
        var character = new Character();
        var faction = new Faction("Bakers of Doom");
        
        // Act
        character.JoinFaction(faction);
        
        // Assert
        Assert.Equal("Bakers of Doom", character.Factions[0].Name);
        Assert.Equal(character, faction.Members[0]);
    }
    
    [Fact]
    public void Character_CanJoinFaction_WhenPartOfFaction()
    {
        // Arrange
        var character = new Character();
        var faction1 = new Faction("Bakers of Doom");
        var faction2 = new Faction("Guild of Awesomeness");
        
        // Act
        character.JoinFaction(faction1);
        character.JoinFaction(faction2);
        
        // Assert
        Assert.Equal("Guild of Awesomeness", character.Factions[1].Name);
        Assert.Equal(character, faction2.Members[0]);
    }
    
    [Fact]
    public void Character_CanLeaveFaction_WhenPartOfFaction()
    {
        // Arrange
        var character = new Character();
        var faction = new Faction("Guild of Awesomeness");
        
        // Act
        character
            .JoinFaction(faction)
            .LeaveFaction(faction);
        
        // Assert
        Assert.Empty(character.Factions);
        Assert.Empty(faction.Members);
    }

    [Fact]
    public void Character_IsAlly_WithCharactersFromSameFaction()
    {
        // Arrange
        var faction = new Faction("Guild of Awesomeness");

        // Act
        var character = new Character().JoinFaction(faction);
        var character2 = new Character().JoinFaction(faction);
        
        // Assert
        Assert.True(character.IsAlly(character2));
    }
    
    [Fact]
    public void Character_CannotDamangeAlly()
    {
        // Arrange
        var startingHealth = 1000;
        var dmgAmt = 100;
        var faction = new Faction("Guild of Awesomeness");
        var character = new Character()
            .JoinFaction(faction);
        var character2 = new Character()
            .SetHealth(startingHealth)
            .JoinFaction(faction);

        // Act
        character.Attack(character2, dmgAmt);
        
        // Assert
        Assert.Equal(startingHealth, character2.Health);
    }

    [Fact]
    public void Character_CanHealAlly()
    {
        // Arrange
        var startingHealth = 1;
        var healAmt = 99;
        var faction = new Faction("Guild of Awesomeness");
        var character = new Character()
            .JoinFaction(faction);
        var character2 = new Character()
            .SetHealth(startingHealth)
            .JoinFaction(faction);

        // Act
        character.Heal(character2, healAmt);
        
        // Assert
        Assert.Equal(startingHealth + healAmt, character2.Health);
    }
    
    [Fact]
    public void Character_CanAttackThing_WhenInRange()
    {
        // Arrange
        var startingHealth = 1000;
        var dmgAmt = 100;
        var characterFactory = new CharacterFactory();
        var character = characterFactory
            .Create(CharacterType.Melee)
            .SetPosition(0, 0);
        var targetThing = new Thing("Tree")
            .SetHealth(startingHealth)
            .SetPosition(1, 1); 
        
        // Act
        character.Attack(targetThing, dmgAmt);
        
        // Assert
        Assert.Equal(startingHealth - dmgAmt, targetThing.Health);
    }

    [Fact]
    public void Thing_IsDestroyed_WhenHealthIs0()
    {
        // Arrange
        var startingHealth = 1;
        var dmgAmt = 100;
        var character = new Character();
        var targetThing = new Thing("Tree")
            .SetHealth(startingHealth);
        
        // Act
        character.Attack(targetThing, dmgAmt);
        
        // Assert
        Assert.True(!targetThing.IsAlive);
    }
}