



using System.Linq;

public class SkillManager
{
    public ShipController shipController;

    public Skill[] allSkills;


    public SkillManager(ShipController shipController)
    {
        this.shipController = shipController;
    }

    public void addSkills(Skill[] skills)
    {
        allSkills = skills;
        foreach (Skill skill in skills)
        {
            skill.shipController = shipController;
        }
    }

    public Skill getLearnableSkill()
    {
        throw new System.NotImplementedException();
    }


    public bool checkLearnable(Skill skill)
    {


        if (shipController.lvl < skill.minLevel) return false;
        Skill requiredSkill = getSkillByName(skill.requiredSkillName);
        if (requiredSkill == null) return true;
        if (!allSkills.Contains(requiredSkill)) throw new System.Exception("Skill not found");
        if (requiredSkill is UnlockSkill)
        {
            return (requiredSkill as UnlockSkill).learned;
        }
        if (requiredSkill is LevelingSkill)
        {
            return (requiredSkill as LevelingSkill).lvl > 0;
        }
        throw new System.Exception("Skill not found");
    }


    public void learnSkill(string skillName)
    {
        Skill skill = getSkillByName(skillName);
        learnSkill(skill);
    }
    public void learnSkill(Skill skill)
    {
        if (!checkLearnable(skill)) return;
        if (skill is UnlockSkill)
        {
            (skill as UnlockSkill).learned = true;
        }
        if (skill is LevelingSkill)
        {
            (skill as LevelingSkill).lvl++;
        }

        if (skill.weaponController != null)
        {
            skill.weaponController.learnSkill(skill.name);
        }
        else if (skill.shipController != null)
        {
            skill.shipController.learnSkill(skill.name);
        }

    }

    public Skill getSkillByName(string name)
    {
        foreach (Skill skill in allSkills)
        {
            if (skill.name == name)
            {
                return skill;
            }
        }
        return null;
    }


}
