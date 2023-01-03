using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillCode
{
    NORMALARROW = 0,
    ARCANESHIFT,
    THROWROCK,
    FALLBOOK,
    FIRECIRCLE,
    RAZOR,
    CHASINGMISSILE,
    SUMMONZOMBIE,
    BARRICADE,
    ICESHEET,
    MIRROR,
    GRABBOT,
    BLOCKWALL
}

public enum SkillLevel
{
    LEVEL1 = 0,
    LEVEL2,
    LEVEL3
}

public class SkillDB : MonoBehaviour
{
    private static SkillDB instance;
    public static SkillDB Instance { get => instance; }

    [SerializeField]
    public Dictionary<SkillCode, SkillCommand[]> skillsDic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            skillsDic = new Dictionary<SkillCode, SkillCommand[]>();

            InitializeSkills();
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void InitializeSkills()
    {
        SkillCommand[] skillCommands = GetComponentsInChildren<SkillCommand>();

        foreach (SkillCommand skillCommand in skillCommands)
        {
            if (skillsDic.ContainsKey(skillCommand.SkillInfo.skillCode))
            {
                skillsDic[skillCommand.SkillInfo.skillCode][(int)skillCommand.SkillInfo.level] = skillCommand;
            }
            else
            {
                SkillCommand[] skillCommandsArr = new SkillCommand[3];
                skillCommandsArr[(int)skillCommand.SkillInfo.level] = skillCommand;
                skillsDic.Add(skillCommand.SkillInfo.skillCode, skillCommandsArr);
            }
        }
    }

    public SkillCommand GetSkill(SkillCode skillCode, SkillLevel level)
    {
        return skillsDic[skillCode][(int)level];
    }
}
