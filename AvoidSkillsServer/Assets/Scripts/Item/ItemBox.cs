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

    [SerializeField]
    private int destroyTime;
    [SerializeField]
    private GameObject itemBallPrefab;

    void Start()
    {
        level = 0;
        id = nextItemBoxId;
        ++nextItemBoxId;
        itemBoxes.Add(id, this);

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
            yield return new WaitForSeconds(10f);
            ++level;

            ServerSend.LevelUpItemBox(id);
        }
    }

    private void Destroy()
    {
        itemBoxes.Remove(id);
        GameObject _itemBall = Instantiate(itemBallPrefab, transform.position, Quaternion.identity);
        _itemBall.GetComponent<ItemBall>().Initialize(level);

        Destroy(gameObject);
        ServerSend.DestroyItemBox(id);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "NormalAttack")
        {
            Destroy();
        }
    }

    public static void Clear()
    {

        foreach (ItemBox item in itemBoxes.Values)
        {
            Destroy(item.gameObject);
        }

        itemBoxes.Clear();
        nextItemBoxId = 1;
    }
}
