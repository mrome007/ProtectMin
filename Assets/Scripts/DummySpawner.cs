using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour 
{
    //TODO FIGURE OUT A BETTER WAY TO SPAWN.
    [SerializeField]
    private MinsPool MinsPool;

    private void Start()
    {
        Invoke("GetTheMins", 0.02f);
    }

    private void GetTheMins()
    {
        for(int index = 0; index < 10; index++)
        {
            var min = MinsPool.GetMins(MinsType.Block);
            var position = transform.position;
            position.x = Random.Range(position.x - 0.5f, position.x + 0.5f);
            min.transform.position = position;
            min.transform.parent = transform;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);
    }
}
