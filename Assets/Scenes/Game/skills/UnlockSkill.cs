public class UnlockSkill : Skill
{
    public bool learned = false;


    public UnlockSkill(string name, int minLevel, string requiredSkill) : base(name, minLevel, requiredSkill)
    {
    }

    public UnlockSkill(string name, int minLevel, string requiredSkill, WeaponController weaponController) : base(name, minLevel, requiredSkill, weaponController)
    {
    }

    public UnlockSkill(string name, int minLevel, string requiredSkill, ShipStats shipStats) : base(name, minLevel, requiredSkill, shipStats)
    {
    }
}
