


using UnityEngine;

public class ShipStats
{
    public float rotationSpeed = 110.0f;
    public float speed = 3f;
    public int exp = 0;
    public int expToNextLvl = 3;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float maxSpeed = 20f;

    public float attackSpeed = 0.8f;




    public Skill[] getSkills()
    {
        return new Skill[] {
            new LevelingSkill("rotationSpeed", 0, "",this),
            new LevelingSkill("maxHealth", 0, "",this),
            new LevelingSkill("attackSpeed", 0, "",this),
            new LevelingSkill("speed", 0, "",this),
        };
    }

    public void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "rotationSpeed":
                this.rotationSpeed += 40;
                Debug.Log("rotationSpeed learnd");
                break;
            case "maxHealth":
                this.maxHealth += 10;
                Debug.Log("maxHealth learnd");
                break;
            case "attackSpeed":
                this.attackSpeed += 0.3f;
                Debug.Log("attackSpeed learnd");
                break;
            case "speed":
                this.speed += 0.5f;
                Debug.Log("speed learnd");
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }
}
