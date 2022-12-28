using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotList
{
    private const int SIZE = 3;
    private int next = 0;
    private bool[] slot;

    public SlotList(){
        slot = new bool[SIZE];
        for (int i = 0; i < 3;++i) slot[i]=false;
    }

    public int add(){
        slot[next] = true;
        nextIndexUpdate();
        return next + 2;
    }

    public void delete(int index){
        slot[index + 2] = false;
        nextIndexUpdate();
    }

    public void nextIndexUpdate(){
        for (int i = 0; i < 3;++i){
            if (!slot[i]){
                next = i;
                return;
            }
        }
        Debug.Log("아이템 슬롯이 가득 차있습니다!");
    }
}
