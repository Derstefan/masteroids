﻿public class LevelingSkill : Skill
{
    public int lvl = 0;


    public LevelingSkill(string name, int minLevel, string requiredSkill) : base(name, minLevel, requiredSkill)
    {

    }

    public LevelingSkill(string name, int minLevel, string requiredSkill, WeaponController weaponController) : base(name, minLevel, requiredSkill, weaponController)
    {

    }

    public LevelingSkill(string name, int minLevel, string requiredSkill, ShipStats shipStats) : base(name, minLevel, requiredSkill, shipStats)
    {

    }
}