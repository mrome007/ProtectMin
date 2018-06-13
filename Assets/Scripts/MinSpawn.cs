using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinSpawn : MonoBehaviour 
{
    public MinsType MinTypeToSpawn;
    public int NumberOfMinsToSpawn;


    public void SpawnMins(MinsPool pool)
    {      
        for(int index = 0; index < NumberOfMinsToSpawn; index++)
        {
            var min = pool.GetMins(MinTypeToSpawn);
            if(min == null)
            {
                continue;
            }
            var position = transform.position;
            position.x = Random.Range(position.x - 1f, position.x + 1f);
            position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
            min.transform.position = position;
            min.transform.parent = null;
        }
    }
}
