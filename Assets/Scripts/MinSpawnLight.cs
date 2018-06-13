using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinSpawnLight : MonoBehaviour 
{
    public MinsType MinTypeToSpawn;
    public bool MinsInBlock;
    public int NumberOfMinsToSpawn;

    [SerializeField]
    private SpriteRenderer blockSprite;

    private MinSpawn minSpawn;

    public void Initialize(MinSpawn minSpawnReference)
    {
        minSpawn = minSpawnReference;
    }

    public void ShowMinSpawn(bool show)
    {
        blockSprite.enabled = show;
    }

    private void OnEnable()
    {
        ShowMinSpawn(true);
    }

    private void Start()
    {
        ShowMinSpawn(MinsInBlock);
        if(!MinsInBlock)
        {
            SpawnMins();
        }
    }

    public void SpawnMins()
    {   
        for(int index = 0; index < NumberOfMinsToSpawn; index++)
        {
            var min = minSpawn.MinsPool.GetMins(MinTypeToSpawn);
            if(min == null)
            {
                continue;
            }
            var position = transform.position;
            position.x = Random.Range(position.x - 1f, position.x + 1f);
            position.z = Random.Range(position.z - 0.5f, position.z + 0.5f);
            min.transform.position = position;
            min.transform.parent = transform;

            var movement = min.GetComponent<MinMovement>();
            movement.SetMovementType(MinMovement.MinMovementType.Spawn);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);

        if(transform.position.x < -15f)
        {
            ReleaseSpawnElements();
        }
    }

    private void ReleaseSpawnElements()
    {
        foreach(Transform child in transform)
        {
            var minLight = child.GetComponent<MinLight>();
            if(minLight != null)
            {
                minSpawn.MinsPool.ReturnMins(minLight);
            }
        }

        minSpawn.MinsPool.ReturnSpawn(this);
    }
}
