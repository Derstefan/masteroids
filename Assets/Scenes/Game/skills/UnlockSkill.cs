using UnityEngine;

public class UnlockSkill : Skill
{
    public bool learned = false;


    public UnlockSkill(string name, int minLevel, string requiredSkill, Sprite sprite ) : base(name, minLevel, requiredSkill, sprite)
    {
    }

    public UnlockSkill(string name, int minLevel, string requiredSkill, Sprite sprite, WeaponController weaponController) : base(name, minLevel, requiredSkill, sprite, weaponController)
    {
    }

    public UnlockSkill(string name, int minLevel, string requiredSkill, Sprite sprite, ShipStats shipStats) : base(name, minLevel, requiredSkill, sprite, shipStats)
    {
    }
}
