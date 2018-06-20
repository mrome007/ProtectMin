using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBlockSpawner : MonoBehaviour 
{
    //TODO FIGURE OUT A BETTER WAY TO SPAWN.
    [SerializeField]
    private MinsPool minsPool;

    [SerializeField]
    private MinsType minType;

    [SerializeField]
    private bool inBlock;

    [SerializeField]
    private float timeToSpawn;

    private void Start()
    {
        Invoke("GetTheBlock", timeToSpawn);
    }

    private void GetTheBlock()
    {
        var minSpawn = minsPool.GetSpawn(minType);
        minSpawn.NumberOfMinsToSpawn = 8;
        minSpawn.MinsInBlock = inBlock;
        var position = transform.position;
        position.x = Random.Range(position.x + 5f, position.x + 10f);
        position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
        minSpawn.transform.position = position;
    }
}
