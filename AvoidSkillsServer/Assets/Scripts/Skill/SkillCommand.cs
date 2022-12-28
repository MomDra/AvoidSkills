using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    public Projectile cmd(Transform player, PlayerStatus status, Vector3 _mousePos);
}

public abstract class SkillCommand : MonoBehaviour, Command
{
    [SerializeField]
    protected SkillInfo skillInfo;
    public SkillInfo SkillInfo { get => skillInfo; }

    public abstract Projectile cmd(Transform _player, PlayerStatus _status, Vector3 _mousePos);
}