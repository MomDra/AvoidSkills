using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A { 
    public int i;

    public A(int _i)
    {
        i = -i;
    }
}


public class Test : MonoBehaviour
{
    Dictionary<int, A> abc = new Dictionary<int, A>();

    // Start is called before the first frame update
    void Start()
    {
        A a = new A(3);

        abc.Add(3, a);

        a.i = 4;

        Debug.Log(abc[3].i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
