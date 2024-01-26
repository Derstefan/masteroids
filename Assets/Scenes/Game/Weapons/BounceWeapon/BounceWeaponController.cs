using UnityEngine;

public class BounceWeaponController : WeaponController
{
    public AudioClip shootSound;

    public override void shoot(Vector3 pos, Quaternion direction)
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);

        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Bouncer", 9, null,this.sprite,this),
            new LevelingSkill("Bouncer-Damage", 11, "Bouncer",this.sprite,this),
            new LevelingSkill("Bouncer-AttackSpeed", 12, "Bouncer",this.sprite, this),
            new LevelingSkill("Bouncer-ProjectileSpeed", 15, "Bouncer",this.sprite, this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Bouncer":
                this.activated = true;
                break;
            case "Bouncer-Damage":
                this.damage += 10;
                break;
            case "Bouncer-AttackSpeed":
                this.attackSpeed -= 0.01f;
                break;
            case "Bouncer-ProjectileSpeed":
                this.projectileSpeed += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
