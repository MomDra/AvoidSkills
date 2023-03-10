using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command{
    public void cmd(GameObject player, PlayerStatus status, Vector3 playerPos);
}

public class NormalArrowCommand : MonoBehaviour, Command
{
    private SkillInfo skillInfo = Resources.Load<SkillInfo>("Skills/NormalArrow");

    public void cmd(GameObject player, PlayerStatus status, Vector3 playerPos){
        Vector3 arrowPos =  playerPos + (MousePointer.Instance.MousePositionInWorld - playerPos).normalized;
        Vector3 velocity = (MousePointer.Instance.MousePositionInWorld - arrowPos).normalized * skillInfo.projectileSpeed;

        GameObject clone = Instantiate(skillInfo.skillPrefab, arrowPos, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = velocity;
    }
}

public class ArcaneShiftCommand : Command{
    private SkillInfo skillInfo = Resources.Load<SkillInfo>("Skills/ArcaneShift");

    public void cmd(GameObject player, PlayerStatus status, Vector3 playerPos){
        status.playerStop = true;
        player.transform.position += (MousePointer.Instance.MousePositionInWorld - playerPos).normalized * skillInfo.range;
    }
}


