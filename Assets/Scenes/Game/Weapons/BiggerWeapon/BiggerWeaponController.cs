using UnityEngine;

public class BiggerWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Bigger Weapon", 0,null),
            new LevelingSkill("Demage Bigger Weapon", 1, "Bigger Weapon"),

                };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Bigger Weapon":
                this.activated = true;
                break;
            case "Demage Bigger Weapon":
                this.damage += 10;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
