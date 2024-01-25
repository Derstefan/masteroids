using UnityEngine;

public abstract class Skill
{
    public string name;

    public int minLevel;
    public string requiredSkillName;
    public Sprite sprite;

    public WeaponController weaponController;
    public ShipStats shipStats;

    public Skill(string name, int minLevel, string requiredSkill, Sprite sprite)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
        this.sprite = sprite;
    }

    public Skill(string name, int minLevel, string requiredSkill, Sprite sprite, WeaponController weaponController)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
        this.sprite = sprite;
        this.weaponController = weaponController;
    }

    public Skill(string name, int minLevel, string requiredSkill, Sprite sprite, ShipStats shipStats)
    {
        this.name = name;
        this.minLevel = minLevel;
        this.requiredSkillName = requiredSkill;
        this.sprite = sprite;
        this.shipStats = shipStats;
    }
}
