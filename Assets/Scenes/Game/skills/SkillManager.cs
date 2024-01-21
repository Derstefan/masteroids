



using System.Linq;
using UnityEngine;

public class SkillManager
{
    public ShipController shipController;

    public Skill[] allSkills = new Skill[] { };


    public SkillManager(ShipController shipController)
    {
        this.shipController = shipController;
    }

    public void addStatSkills(Skill[] skills)
    {
        foreach (Skill skill in skills)
        {
            skill.shipStats = shipController.shipStats;
        }
        allSkills = allSkills.Concat(skills).ToArray();
    }

    public void addWeaponSkills(Skill[] skills)
    {
        allSkills = allSkills.Concat(skills).ToArray();

    }

    public Skill getLearnableSkill()
    {
        Skill[] skills = allSkills.Where(skill => checkLearnable(skill)).ToArray();
        int randomIndex = Random.Range(0, skills.Length);
        return skills[randomIndex];
    }

    public string getAllLearnableSkills()
    {
        return string.Join(", ", allSkills.Where(skill => checkLearnable(skill)).Select(skill => skill.name).ToArray());
    }


    public bool checkLearnable(Skill skill)
    {
        if (shipController.lvl < skill.minLevel) return false;
        if (skill is UnlockSkill)
        {
            if ((skill as UnlockSkill).learned)
            {
                return false;
            }
        }
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
        else if (skill.shipStats != null)
        {
            skill.shipStats.learnSkill(skill.name);
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
