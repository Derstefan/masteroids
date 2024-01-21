public class UnlockSkill : Skill
{
    public bool learned = false;


    public UnlockSkill(string name, int minLevel, string requiredSkill) : base(name, minLevel, requiredSkill)
    {
    }
}
