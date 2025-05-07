using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMS_Skill
{
    public string skillName;
    public string skillDescription;
    public int skillLevel;

    public PMS_Skill(string skillName, string skillDescription, int skillLevel = 0)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription;
        this.skillLevel = skillLevel;
    }
}


