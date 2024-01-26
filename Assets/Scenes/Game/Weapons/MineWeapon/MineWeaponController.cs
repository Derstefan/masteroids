using UnityEngine;

public class MineWeaponController : WeaponController
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
            new UnlockSkill("Mine", 7,null,this.sprite,this),
            new LevelingSkill("Mine-Damage", 10, "Mine",this.sprite, this),
            new LevelingSkill("Mine-ExplosionRadius", 10, "Mine",this.sprite, this),
            new LevelingSkill("Mine-AttackSpeed", 20, "Mine",this.sprite, this),
            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Mine":
                this.activated = true;
                break;
            case "Mine-Damage":
                this.damage += 5;
                break;
            case "Mine-ExplosionRadius":
                this.explosionRadius += 0.5f;
                break;
            case "Mine-AttackSpeed":
                this.attackSpeed += 0.2f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

    public void createShootNew(Vector3 pos, Quaternion direction)
    {
        GameObject o = Instantiate(projectilePrefab, pos, direction);
        o.GetComponent<MineProjectileController>().damage = damage;
        o.GetComponent<MineProjectileController>().force = projectileSpeed;
        o.GetComponent<MineProjectileController>().lifeTime = lifeTime;
        o.GetComponent<MineProjectileController>().explosionRadius = explosionRadius;
        o.GetComponent<MineProjectileController>().explosionDamage = explosionDamage;
        o.GetComponent<MineProjectileController>().goProjectile();
    }

}
