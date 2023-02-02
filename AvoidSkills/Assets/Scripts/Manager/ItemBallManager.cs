using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBallManager : MonoBehaviour
{
    public int id;
    [HideInInspector]
    public SkillCode skillCode;
    [HideInInspector]
    public SkillLevel skillLevel;

    public void Initialize(int _id, SkillCode _skillCode, SkillLevel _skillLevel)
    {
        id = _id;
        skillCode = _skillCode;
        skillLevel = _skillLevel;
    }

    public void Destory()
    {
        Destroy(gameObject);
    }

}
