using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ATTACK,
    BUFF,
    UTILITY
}

[CreateAssetMenu(menuName = "SkillInfo")]
public class SkillInfo : ScriptableObject
{
    public SkillCode skillCode;
    public string skillName;
    public int damage;
    public int range;
    public int speed;
    public float delay;
    public float duration;
    public SkillLevel level;
    public int coolDownTime;
    public SkillType skillType;




    public GameObject skillPrefab;
}
