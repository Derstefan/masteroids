using UnityEngine;

public class LevelingSkill : Skill
{
    public int lvl = 0;


    public LevelingSkill(string name, int minLevel, string requiredSkill, Sprite sprite) : base(name, minLevel, requiredSkill, sprite)
    {

    }

    public LevelingSkill(string name, int minLevel, string requiredSkill, Sprite sprite, WeaponController weaponController) : base(name, minLevel, requiredSkill, sprite, weaponController)
    {

    }

    public LevelingSkill(string name, int minLevel, string requiredSkill, Sprite sprite, ShipStats shipStats) : base(name, minLevel, requiredSkill, sprite, shipStats)
    {

    }
}
