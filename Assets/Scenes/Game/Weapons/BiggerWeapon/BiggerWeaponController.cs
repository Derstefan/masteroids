using UnityEngine;

public class BiggerWeaponController : WeaponController
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
            new UnlockSkill("Canone", 0,null, this.sprite, this),
            new LevelingSkill("Canone-Damage", 0, "Canone", this.sprite, this),
            new LevelingSkill("Canone-AttackSpeed", 0, "Canone", this.sprite, this),
            new LevelingSkill("Canone-ProjectileSpeed", 0, "Canone", this.sprite, this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Canone":
                this.activated = true;
                break;
            case "Canone-Damage":
                this.damage += 10;
                break;
            case "Canone-AttackSpeed":
                this.attackSpeed -= 0.01f;
                break;
            case "Canone-ProjectileSpeed":
                this.projectileSpeed += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
