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

    private bool isAvailable = true;

    private IEnumerator CoolDownTimerCoroutine(){
        isAvailable = false;
        yield return new WaitForSeconds(skillInfo.coolDownTime);
        isAvailable = true;
    }

    public void command(Transform player, PlayerStatus status){
        if (isAvailable)
        {
            StartCoroutine(CoolDownTimerCoroutine());
            cmd(player, status);
        }
        else Debug.Log(skillInfo.skillName + "이(기) 아직 준비되지 않았습니다! ");
    }

    public abstract void cmd(Transform player, PlayerStatus status);
}