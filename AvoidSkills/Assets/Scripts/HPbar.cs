using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,1.5f,0);
    public Unit unit;

    void LateUpdate()
    {
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(unit.transform.position + new Vector3(0,1.5f,0));
    }
}
