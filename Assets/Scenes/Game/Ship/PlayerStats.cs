


using UnityEngine;

public class ShipStats
{
    public float rotationSpeed = 170.0f;
    public float thrustForce = 3f;
    public int pointsCollected = 0;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float attackSpeed = 0.2f;




    public Skill[] getSkills()
    {
        return new Skill[] {
            new LevelingSkill("rotationSpeed", 0, ""),
            new LevelingSkill("maxHealth", 0, ""),
            new LevelingSkill("attackSpeed", 0, ""),
            new LevelingSkill("thrustForce", 0, ""),
        };
    }

    public void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "rotationSpeed":
                Debug.Log("rotationSpeed learnd");
                this.rotationSpeed += 10;
                break;
            case "maxHealth":
                this.maxHealth += 10;
                break;
            case "attackSpeed":
                this.attackSpeed -= 0.01f;
                break;
            case "thrustForce":
                this.thrustForce += 0.1f;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }
}
