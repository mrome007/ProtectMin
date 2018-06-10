﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBlockSpawner : MonoBehaviour 
{
    //TODO FIGURE OUT A BETTER WAY TO SPAWN.
    [SerializeField]
    private MinsPool MinsPool;

    [SerializeField]
    private MinsType minType;

    private void Start()
    {
        Invoke("GetTheBlock", 0.05f);
    }

    private void GetTheBlock()
    {
        var minSpawn = MinsPool.GetSpawn(minType);
        var position = transform.position;
        position.x = Random.Range(position.x + 5f, position.x + 10f);
        position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
        minSpawn.transform.position = position;
        minSpawn.transform.parent = transform;
    }
}
