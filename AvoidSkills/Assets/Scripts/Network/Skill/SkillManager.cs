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

        private SlotList slotList;

        public SkillCommand[] skillCommands { get; private set; } // 1. NormalAttack 2. UserSkill 3~5. itemSkills

        private SkillUIController skillUIController;
        float[] coolTime; // 1. NormalAttack 2. UserSkill 3~5. itemSkills

        private void Awake()
        {
            playerTransform = transform;
            playerStatus = GetComponent<PlayerStatus>();
            // skillUIManager = GetComponent<SkillUIManager>();

            slotList = new SlotList();
            skillCommands = new SkillCommand[5];
            skillUIController = GetComponent<SkillUIController>();
            coolTime = new float[5];

            skillCommands[0] = SkillDB.Instance.GetSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1);
            skillCommands[1] = SkillDB.Instance.GetSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1);

            skillUIController.SetSkillImage(skillCommands[0].SkillInfo.skillImage, 0);
            skillUIController.SetSkillImage(skillCommands[1].SkillInfo.skillImage, 1);
        }

        private IEnumerator CoolTimeCoroutine(float _time, int _index)
        {
            coolTime[_index] = _time;
            float _maxTime = _time;

            while (!CheckCoolTime(_index))
            {
                yield return new WaitForSeconds(0.1f);
                coolTime[_index] -= 0.1f;
                skillUIController.SetCoolTimeGauge(coolTime[_index], _maxTime, _index);
            }

            coolTime[_index] = 0f;
            skillUIController.SetCoolTimeGauge(coolTime[_index], _maxTime, _index);
        }

        private bool CheckCoolTime(int _index)
        {
            return coolTime[_index] <= 0.00001f ? true : false;
        }

        private void SetCoolTime(float _time, int _index)
        {
            if (_time < 0f || _index < 0 || _index >= coolTime.Length) throw new System.ArgumentException();

            StartCoroutine(CoolTimeCoroutine(_time, _index));
        }

        private void CountCheck(int i)
        {
            if (skillCommands[i].SkillInfo.useType != UseType.Permanent)
            {
                if (skillCommands[i].SkillInfo.useType == UseType.MultipleTimes) skillCommands[i].currUsableCount--; // 멀티플 할 때 보자구~~
                if (skillCommands[i].currUsableCount == 0) deleteItem(i);
            }
        }

        public void addItem(SkillCode _skillCode, SkillLevel _skillLevel)
        {
            int index = slotList.add();
            skillCommands[index] = SkillDB.Instance.GetSkill(_skillCode, _skillLevel);
            skillUIController.SetSkillImage(skillCommands[index].SkillInfo.skillImage, index);
        }

        private void deleteItem(int index)
        {
            slotList.delete(index);
            skillCommands[index] = null;
            // skillUIManager.deleteSkillUI(index);
        }

        public void NormalAttack()
        {
            if (CheckCoolTime(0))
            {
                ClientSend.ShootSkill(SkillCode.NORMALARROW, SkillLevel.LEVEL1, MousePointer.Instance.MousePositionInWorld);
                SetCoolTime(skillCommands[0].SkillInfo.coolDownTime, 0);
            }
        }

        public void UserCustomSkill()
        {
            if (CheckCoolTime(1))
            {
                ClientSend.ShootSkill(SkillCode.ARCANESHIFT, SkillLevel.LEVEL1, MousePointer.Instance.MousePositionInWorld);
                SetCoolTime(skillCommands[1].SkillInfo.coolDownTime, 1);
            }
        }

        public void ItemSkill1()
        {
            if (skillCommands[2] != null && CheckCoolTime(2))
            {
                ClientSend.ShootSkill(skillCommands[2].SkillInfo.skillCode, skillCommands[2].SkillInfo.level, MousePointer.Instance.MousePositionInWorld);
                SetCoolTime(skillCommands[2].SkillInfo.coolDownTime, 2);
                // CountCheck(2);
            }
        }

        public void ItemSkill2()
        {
            if (skillCommands[3] != null && CheckCoolTime(3))
            {
                ClientSend.ShootSkill(skillCommands[3].SkillInfo.skillCode, skillCommands[3].SkillInfo.level, MousePointer.Instance.MousePositionInWorld);
                SetCoolTime(skillCommands[3].SkillInfo.coolDownTime, 3);
            }
        }

        public void ItemSkill3()
        {
            if (skillCommands[4] != null && CheckCoolTime(4))
            {
                ClientSend.ShootSkill(skillCommands[4].SkillInfo.skillCode, skillCommands[4].SkillInfo.level, MousePointer.Instance.MousePositionInWorld);
                SetCoolTime(skillCommands[4].SkillInfo.coolDownTime, 4);
            }
        }
    }
}


