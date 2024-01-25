using UnityEngine;

public class ImplosionWeaponController : WeaponController
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
            new UnlockSkill("Implosion Granade", 20,null,this.sprite, this),
            new LevelingSkill("Implosion Granade Damage", 21, "Implosion Granade",this.sprite, this),
            new LevelingSkill("Implosion Granade Explosion Radius", 22, "Implosion Granade",this.sprite, this),
            new LevelingSkill("Implosion Granade AttackSpeed", 23, "Implosion Granade",this.sprite, this)
        };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Implosion Weapon":
                this.activated = true;
                break;
            case "Implosion Granade Damage":
                this.damage += 5;
                break;
            case "Implosion Granade Explosion Radius":
                this.explosionRadius += 0.5f;
                break;
            case "Implosion Granade AttackSpeed":
                this.attackSpeed += 0.2f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

    public void createShootNew(Vector3 pos, Quaternion direction)
    {
        GameObject o = Instantiate(projectilePrefab, pos, direction);
        o.GetComponent<ImplosionProjectileController>().damage = damage;
        o.GetComponent<ImplosionProjectileController>().force = projectileSpeed;
        o.GetComponent<ImplosionProjectileController>().lifeTime = lifeTime;
        o.GetComponent<ImplosionProjectileController>().explosionRadius = explosionRadius;
        o.GetComponent<ImplosionProjectileController>().explosionDamage = explosionDamage;
        o.GetComponent<ImplosionProjectileController>().goProjectile();
    }

}
