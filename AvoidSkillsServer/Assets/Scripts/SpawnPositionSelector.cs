using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionSelector
{
    bool blueSpawned;
    bool redSpawned;

    public Vector3 GetSpawnPos(bool _isRed)
    {
        if (!_isRed)
        {
            if (!redSpawned)
            {
                return new Vector3(10f, 1.0f, 3f);
            }
            else
            {
                return new Vector3(10f, 1.0f, -3f);
            }


        }
        else
        {
            if (!blueSpawned)
            {
                return new Vector3(-10f, 1.0f, 3f);
            }
            else
            {
                return new Vector3(-10f, 1.0f, -3f);
            }
        }
    }

    public void Clear()
    {
        redSpawned = false;
        blueSpawned = false;
    }
}
