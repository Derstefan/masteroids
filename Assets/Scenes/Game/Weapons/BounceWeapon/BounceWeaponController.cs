using UnityEngine;

public class BounceWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Bounce Weapon", 10,null,this.sprite,this),
            new LevelingSkill("Damage Bigger Weapon", 0, "Bounce Weapon",this.sprite,this),
            new LevelingSkill("AttackSpeed Bigger Weapon", 0, "Bounce Weapon",this.sprite, this),
            new LevelingSkill("Projectil Speed Bigger Weapon", 0, "Damage Bounce Weapon",this.sprite, this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Bounce Weapon":
                this.activated = true;
                break;
            case "Damage Bounce Weapon":
                this.damage += 10;
                break;
            case "AttackSpeed Bounce Weapon":
                this.attackSpeed -= 0.01f;
                break;
            case "Projectil Speed Bounce Weapon":
                this.projectileSpeed += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
