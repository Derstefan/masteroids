using UnityEngine;

public class SimpleWeaponController : WeaponController
{
    public AudioClip shootSound;

    public override void shoot(Vector3 pos, Quaternion direction)
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);

        createShoot(pos, direction);
    }



    public override Skill[] getWeaponSkills()
    {
        return new Skill[] {
        new UnlockSkill("Gunn",0,null, this.sprite, this),
        new LevelingSkill("Gunn-Damage",0,"Gunn", this.sprite, this),
        new LevelingSkill("Gunn-AttackSpeed",0,"Gunn", this.sprite, this),
        };

    }

    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Gunn":
                this.activated = true;
                break;
            case "Gunn-Damage":
                this.damage += 10;
                break;
            case "Gunn-AttackSpeed":
                this.attackSpeed += 0.6f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

}
