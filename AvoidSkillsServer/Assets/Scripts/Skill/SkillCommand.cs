using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    public void cmd(Player player, PlayerStatus status, Vector3 _mousePos);
}

public abstract class SkillCommand : MonoBehaviour, Command
{
    [SerializeField]
    protected SkillInfo skillInfo;
    public SkillInfo SkillInfo { get => skillInfo; }

    public abstract void cmd(Player _player, PlayerStatus _status, Vector3 _mousePos);
}