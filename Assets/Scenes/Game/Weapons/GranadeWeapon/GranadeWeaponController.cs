using UnityEngine;

public class GranadeWeaponController : WeaponController
{
    public float explosionRadius;
    public float explosionDamage;
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShootNew(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Granade", 5,null,this.sprite,this),
            new LevelingSkill("Granade-Damage", 8, "Granade",this.sprite, this),
            new LevelingSkill("Granade-ExplosionRadius", 9, "Granade",this.sprite, this),
            new LevelingSkill("Granade-AttackSpeed", 20, "Granade",this.sprite, this),
            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Granade":
                this.activated = true;
                break;
            case "Granade-Damage":
                this.damage += 5;
                break;
            case "Granade-ExplosionRadius":
                this.explosionRadius += 0.5f;
                break;
            case "Granade-AttackSpeed":
                this.attackSpeed += 0.2f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

    public void createShootNew(Vector3 pos, Quaternion direction)
    {
        GameObject o = Instantiate(projectilePrefab, pos, direction);
        o.GetComponent<GranadeProjectileController>().damage = damage;
        o.GetComponent<GranadeProjectileController>().force = projectileSpeed;
        o.GetComponent<GranadeProjectileController>().lifeTime = lifeTime;
        o.GetComponent<GranadeProjectileController>().explosionRadius = explosionRadius;
        o.GetComponent<GranadeProjectileController>().explosionDamage = explosionDamage;
        o.GetComponent<GranadeProjectileController>().goProjectile();
    }

}
