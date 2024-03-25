namespace RPGCombat.Models;

public class Faction(string name)
{
    public string Name => name;
    public List<Character> Members { get; set; } = new List<Character>();
    
    public void AddMember(Character character)
    {
        if(this.Members.Contains(character))
            return;
        
        Members.Add(character);
    }
    
    public void RemoveMember(Character character)
    {
        if(!this.Members.Contains(character))
            return;
        
        Members.Remove(character);
    }
}