using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    public void cmd(Transform player, PlayerStatus status);
}

public abstract class SkillCommand : MonoBehaviour, Command
{
    [SerializeField]
    protected SkillInfo skillInfo;
    public SkillInfo SkillInfo { get => skillInfo; }
    public int currUsableCount{ get; set; }

    private void Awake() {
        currUsableCount = skillInfo.usableCount;
    }

    public abstract void cmd(Transform player, PlayerStatus status);
}