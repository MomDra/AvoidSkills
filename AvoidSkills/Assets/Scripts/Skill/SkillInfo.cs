using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int projectileSpeed;
    public float delay;
    public SkillLevel level;
    public float coolDownTime;
    public SkillType skillType;
    public Sprite skillImage;
    public GameObject skillPrefab;

}
