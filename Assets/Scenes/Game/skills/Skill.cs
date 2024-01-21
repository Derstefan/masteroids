public abstract class Skill
{
    public string name;

    public int minLevel;
    public string requiredSkillName;

    public WeaponController weaponController;
    public ShipController shipController;

    public Skill(string name, int minLevel, string requiredSkill)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
    }
}
