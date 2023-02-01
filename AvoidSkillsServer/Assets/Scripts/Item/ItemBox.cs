using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public static Dictionary<int, ItemBox> itemBoxes = new();
    private static int nextItemBoxId = 1;

    public int id;
    private int level;

    bool isRedBox;

    void Start()
    {
        level = 1;
        id = nextItemBoxId;
        ++nextItemBoxId;
        itemBoxes.Add(id, this);

        Debug.Log("key: " + id);

        ServerSend.InstantiateItemBox(this);

        StartCoroutine(LevelUpCoroutine());
    }

    public void Initialize(bool _isRed)
    {
        isRedBox = _isRed;
    }

    private IEnumerator LevelUpCoroutine()
    {
        for (int i = 0; i < 2; ++i)
        {
            yield return new WaitForSeconds(3f);
            ++level;

            ServerSend.LevelUpItemBox(id);
        }
    }
}
