using UnityEngine;

public class TripleSimpleWeaponController : WeaponController
{
    public AudioClip shootSound;
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);

        createShoot(pos, direction * Quaternion.Euler(0, 0, -10));
        createShoot(pos, direction);
        createShoot(pos, direction * Quaternion.Euler(0, 0, 10));
    }


    public override Skill[] getWeaponSkills()
    {
        return new Skill[] {
        new UnlockSkill("Trippple",0,null, this.sprite, this),
        new LevelingSkill("Trippple-Damage",0,"Trippple", this.sprite, this),
        new LevelingSkill("Trippple-AttackSpeed",0,"Trippple", this.sprite, this),
        };
    }

    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Trippple":
                this.activated = true;
                break;
            case "Trippple-Damage":
                this.damage += 10;
                break;
            case "Trippple-AttackSpeed":
                this.attackSpeed += 0.6f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

}
