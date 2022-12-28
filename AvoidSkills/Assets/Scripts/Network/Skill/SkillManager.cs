using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class SkillManager : MonoBehaviour
    {
        private Transform playerTransform;
        private PlayerStatus playerStatus;
        // private SkillUIManager skillUIManager;

        public SlotList slotList { get; private set; }

        public SkillCommand[] skillComands { get; private set; } // 1. NormalAttack 2. UserSkill 3~5. itemSkills
        public CoolDownTimer[] currCoolDowns { get; private set; } // 1. NormalAttack 2. UserSkill 3~5. itemSkills


        private void Awake()
        {
            playerTransform = transform;
            playerStatus = GetComponent<PlayerStatus>();
            // skillUIManager = GetComponent<SkillUIManager>();

            slotList = new SlotList();
            skillComands = new SkillCommand[5];
            currCoolDowns = new CoolDownTimer[5];

            for (int i = 0; i < 5; ++i)
            {
                currCoolDowns[i] = new CoolDownTimer();
            }
            skillComands[0] = SkillDB.Instance.GetSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1);
            skillComands[1] = SkillDB.Instance.GetSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1);
        }

        private IEnumerator CoolDownCoroutine(CoolDownTimer timer)
        {
            while (timer.currTime > 0.00001f)
            {
                yield return new WaitForSeconds(0.1f);
                timer.tik();
            }
            timer.reset();
        }

        private bool CheckCoolDown(int i)
        {
            if (currCoolDowns[i].IsReady)
            {
                currCoolDowns[i].set(skillComands[i].SkillInfo.coolDownTime);
                StartCoroutine(CoolDownCoroutine(currCoolDowns[i]));
                // StartCoroutine(skillUIManager.CoolDownGaugeUpdateCoroutine(i));
                return true;
            }

            Debug.Log(skillComands[i].SkillInfo.skillName + " 이(가) 아직 준비되지 않았습니다!");
            return false;
        }

        private void CountCheck(int i)
        {
            if (skillComands[i].SkillInfo.useType != UseType.Permanent)
            {
                if (skillComands[i].SkillInfo.useType == UseType.MultipleTimes) skillComands[i].currUsableCount--; // 멀티플 할 때 보자구~~
                if (skillComands[i].currUsableCount == 0) deleteItem(i);
            }
        }

        public void addItem(SkillCode code, SkillLevel level)
        {
            int index = slotList.add();
            skillComands[index] = SkillDB.Instance.GetSkill(code, level);
            currCoolDowns[index].reset();
            // skillUIManager.addSkillUI(skillComands[index], index);
        }

        private void deleteItem(int index)
        {
            slotList.delete(index);
            skillComands[index] = null;
            // skillUIManager.deleteSkillUI(index);
        }

        public void NormalAttack()
        {
            if (CheckCoolDown(0))
            {
                ClientSend.ShootSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1, MousePointer.Instance.MousePositionInWorld);
            }
        }

        public void UserCustomSkill()
        {
            if (CheckCoolDown(1))
            {
                ClientSend.ShootSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1, MousePointer.Instance.MousePositionInWorld);
            }
        }

        public void ItemSkill1()
        {
            if (skillComands[2] != null)
            {
                if (CheckCoolDown(2))
                {
                    skillComands[2].cmd(playerTransform, playerStatus);
                    CountCheck(2);
                }
            }
            else Debug.Log("1번 스킬은 비어있습니다!");
        }

        public void ItemSkill2()
        {
            if (skillComands[3] != null)
            {
                if (CheckCoolDown(3))
                {
                    skillComands[3].cmd(playerTransform, playerStatus);
                    CountCheck(3);
                }
            }
            else Debug.Log("2번 스킬은 비어있습니다!");
        }

        public void ItemSkill3()
        {
            if (skillComands[4] != null)
            {
                if (CheckCoolDown(4))
                {
                    skillComands[4].cmd(playerTransform, playerStatus);
                    CountCheck(4);
                }
            }
            else Debug.Log("3번 스킬은 비어있습니다!");
        }
    }
}


