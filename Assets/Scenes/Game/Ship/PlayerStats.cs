using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public float rotationSpeed = 110.0f;
    public float speed = 3f;
    public int exp = 0;
    public int expToNextLvl = 7;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float maxSpeed = 20f;
    public Sprite rotationSpeed_sprite;
    public Sprite speed_sprite;
    public Sprite health_sprite;
    public Sprite attackSpeed_sprite;

    public float attackSpeed = 0.1f;

    public float falterDuration = 1.3f; // duration to get control back after hit with asteroid

    public float collectMinDistance = 5f;

    public ShipStats(Sprite rotationSpeed, Sprite speed, Sprite health, Sprite attackSpeed)
    {
        this.rotationSpeed_sprite = rotationSpeed;
        this.speed_sprite = speed;
        this.health_sprite = health;
        this.attackSpeed_sprite = attackSpeed;
    }

    public Skill[] getSkills()
    {
        return new Skill[] {
            new LevelingSkill("rotationSpeed", 0, "", this.rotationSpeed_sprite, this),
            new LevelingSkill("maxHealth", 0, "", this.health_sprite, this),
            new LevelingSkill("attackSpeed", 0, "", this.attackSpeed_sprite, this),
            new LevelingSkill("speed", 0, "", this.speed_sprite, this),
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
                this.currentHealth += 10;
                Debug.Log("maxHealth learnd");
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
