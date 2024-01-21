public abstract class Skill
{
    public string name;

    public int minLevel;
    public string requiredSkillName;

    public WeaponController weaponController;
    public ShipStats shipStats;

    public Skill(string name, int minLevel, string requiredSkill)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
    }

    public Skill(string name, int minLevel, string requiredSkill, WeaponController weaponController)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
        this.weaponController = weaponController;
    }

    public Skill(string name, int minLevel, string requiredSkill, ShipStats shipStats)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
        this.shipStats = shipStats;
    }
}
