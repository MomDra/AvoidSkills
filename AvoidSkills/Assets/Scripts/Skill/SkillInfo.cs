using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType{
        ATTACK,
        BUFF,
        UTILITY
}

[CreateAssetMenu(menuName = "Skill")]
public class SkillInfo : ScriptableObject
{
    public string skillName;
    public int damage;
    public int range;
    public int projectileSpeed;
    public float delay;
    public int level;
    public int coolDownTime;
    public SkillType skillType;
    



    public GameObject skillPrefab;
}
