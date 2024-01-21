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
            new UnlockSkill("Bigger Weapon", 0,null,this),
            new LevelingSkill("Damage Bigger Weapon", 0, "Bigger Weapon",this),
            new LevelingSkill("AttackSpeed Bigger Weapon", 0, "Bigger Weapon",this),
            new LevelingSkill("Projectil Speed Bigger Weapon", 0, "Damage Bigger Weapon",this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Bigger Weapon":
                this.activated = true;
                break;
            case "Damage Bigger Weapon":
                this.damage += 10;
                break;
            case "AttackSpeed Bigger Weapon":
                this.attackSpeed -= 0.01f;
                break;
            case "Projectil Speed Bigger Weapon":
                this.projectileSpeed += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
