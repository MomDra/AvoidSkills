using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UseType
{
    Permanent,
    OnlyOnce,
    MultipleTimes
}

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
    public float range;
    public int speed;
    public float delay;
    public SkillLevel level;
    public float coolDownTime;
    public SkillType skillType;
    public Sprite skillImage;
    public GameObject skillPrefab;
    public GameObject[] subPrefabs;
    public UseType useType;
    public int usableCount;
    public string description;
}
