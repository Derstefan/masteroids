using UnityEngine;

public class ReboundWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Rebound Weapon", 0,null,this),
            new LevelingSkill("Damage Rebound Weapon", 0, "Rebound Weapon",this),
            new LevelingSkill("AttackSpeed Rebound Weapon", 0, "Rebound Weapon",this),
            new LevelingSkill("Projectil Speed Rebound Weapon", 0, "Rebound Weapon",this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Rebound Weapon":
                this.activated = true;
                break;
            case "Damage Rebound Weapon":
                this.damage += 10;
                break;
            case "AttackSpeed Rebound Weapon":
                this.attackSpeed -= 0.01f;
                break;
            case "Projectil Speed Rebound Weapon":
                this.projectileSpeed += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
