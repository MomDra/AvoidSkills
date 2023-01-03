using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotList
{
    private int size = 0;
    private int next = 0;
    private bool[] slot;

    public SlotList()
    {
        slot = new bool[3];
    }

    public bool isFull()
    {
        return size == 3;
    }

    public int add()
    {
        ++size;
        slot[next] = true;
        int tmp = next + 2;
        nextIndexUpdate();
        return tmp;
    }

    public void delete(int index)
    {
        --size;
        slot[index - 2] = false;
        nextIndexUpdate();
    }

    private void nextIndexUpdate()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (!slot[i])
            {
                next = i;
                return;
            }
        }
    }
}
