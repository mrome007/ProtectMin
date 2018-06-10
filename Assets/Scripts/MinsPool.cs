using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinsPool : MonoBehaviour 
{
    [SerializeField]
    private List<Min> MinsMainObjects;
    
    [SerializeField]
    private List<MinLight> MinsLightObjects;

    [SerializeField]
    private List<MinSpawn> MinsSpawnObjects;

    [SerializeField]
    private int NumberOfMinObjectsPerPool = 10;

    [SerializeField]
    private int NumberOfMinSpawnObjectsPerPool = 2;

    [SerializeField]
    private Transform MinPoolRoot;

    private Dictionary<MinsType, List<MinLight>> MinsPoolContainer;
    private Dictionary<MinsType, List<MinSpawn>> MinsSpawnConainer;

    private void Awake()
    {
        MinsPoolContainer = new Dictionary<MinsType, List<MinLight>>();
        MinsSpawnConainer = new Dictionary<MinsType, List<MinSpawn>>();
    }

    private void Start()
    {
        PopulatePool();
    }

    public void PopulatePool()
    {
        foreach(var min in MinsMainObjects)
        {
            if(!MinsPoolContainer.ContainsKey(min.MinType))
            {
                MinsPoolContainer.Add(min.MinType, new List<MinLight>());
                for(int count = 0; count < NumberOfMinObjectsPerPool; count++)
                {
                    var minLight = (MinLight)Instantiate(MinsLightObjects[(int)min.MinType]);
                    minLight.transform.parent = MinPoolRoot;
                    minLight.transform.localPosition = Vector3.zero;
                    minLight.Initialize(min);
                    minLight.gameObject.SetActive(false);
                    MinsPoolContainer[min.MinType].Add(minLight);
                }
            }

            if(!MinsSpawnConainer.ContainsKey(min.MinType))
            {
                MinsSpawnConainer.Add(min.MinType, new List<MinSpawn>());
                for(int count = 0; count < NumberOfMinSpawnObjectsPerPool; count++)
                {
                    var minSpawn = (MinSpawn)Instantiate(MinsSpawnObjects[(int)min.MinType]);
                    minSpawn.transform.parent = MinPoolRoot;
                    minSpawn.transform.localPosition = Vector3.zero;
                    minSpawn.gameObject.SetActive(false);
                    MinsSpawnConainer[min.MinType].Add(minSpawn);
                }
            }
        }
    }

    public MinLight GetMins(MinsType minType)
    {
        if(!MinsPoolContainer.ContainsKey(minType))
        {
            return null;
        }

        var minList = MinsPoolContainer[minType];
        MinLight minGot = null;
        foreach(var minLight in minList)
        {
            //TODO find a better way to keep them active/inactive. Active doing their job. Inactive is disabled or just following the player.
            if(!minLight.gameObject.activeSelf)
            {
                minLight.gameObject.SetActive(true);
                minGot = minLight;
                minGot.transform.parent = null;
                break;
            }
        }

        return minGot;
    }

    public MinSpawn GetSpawn(MinsType minType)
    {
        if(!MinsSpawnConainer.ContainsKey(minType))
        {
            return null;
        }

        var minSpawnList = MinsSpawnConainer[minType];
        MinSpawn spawnGot = null;

        foreach(var minSpawn in minSpawnList)
        {
            if(!minSpawn.gameObject.activeSelf)
            {
                minSpawn.gameObject.SetActive(true);
                spawnGot = minSpawn;
                spawnGot.transform.parent = null;
                break;
            }
        }

        return spawnGot;
    }

    public void ReturnMins(MinLight minLight)
    {
        //turn off movement.
        var movement = minLight.GetComponent<MinMovement>();
        movement.SetMovementType(MinMovement.MinMovementType.None);
        movement.enabled = false;

        minLight.gameObject.SetActive(false);
        minLight.transform.parent = MinPoolRoot;
        minLight.transform.localPosition = Vector3.zero;
    }

    public void ReturnSpawn(MinSpawn minSpawn)
    {
        minSpawn.gameObject.SetActive(false);
        minSpawn.transform.parent = MinPoolRoot;
        minSpawn.transform.localPosition = Vector3.zero;
    }

}

public enum MinsType
{
    None = 4,
    Block = 0,
    Floor = 1,
    Burst = 2,
    Projectile = 3
}
