using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinsPool : MonoBehaviour 
{
    [SerializeField]
    private List<Min> MinsMainObjects;

    [SerializeField]
    private MinSpawn MinSpawnMainObject;
    
    [SerializeField]
    private List<MinLight> MinsLightObjects;

    [SerializeField]
    private List<MinSpawnLight> MinsSpawnObjects;

    [SerializeField]
    private int NumberOfMinObjectsPerPool = 10;

    [SerializeField]
    private int NumberOfMinSpawnObjectsPerPool = 2;

    [SerializeField]
    private Transform MinPoolRoot;

    private Dictionary<MinsType, List<MinLight>> MinsPoolContainer;
    private Dictionary<MinsType, List<MinSpawnLight>> MinsSpawnConainer;

    private void Awake()
    {
        MinsPoolContainer = new Dictionary<MinsType, List<MinLight>>();
        MinsSpawnConainer = new Dictionary<MinsType, List<MinSpawnLight>>();
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
                MinsSpawnConainer.Add(min.MinType, new List<MinSpawnLight>());
                for(int count = 0; count < NumberOfMinSpawnObjectsPerPool; count++)
                {
                    var minSpawn = (MinSpawnLight)Instantiate(MinsSpawnObjects[(int)min.MinType]);
                    minSpawn.transform.parent = MinPoolRoot;
                    minSpawn.transform.localPosition = Vector3.zero;
                    minSpawn.Initialize(MinSpawnMainObject);
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
                minLight.GetComponent<Collider>().enabled = true;
                minGot = minLight;
                minGot.transform.parent = null;
                break;
            }
        }

        return minGot;
    }

    public MinSpawnLight GetSpawn(MinsType minType)
    {
        if(!MinsSpawnConainer.ContainsKey(minType))
        {
            return null;
        }

        var minSpawnList = MinsSpawnConainer[minType];
        MinSpawnLight spawnGot = null;

        foreach(var minSpawn in minSpawnList)
        {
            if(!minSpawn.gameObject.activeSelf)
            {
                minSpawn.gameObject.SetActive(true);
                minSpawn.GetComponent<Collider>().enabled = true;
                spawnGot = minSpawn;
                spawnGot.transform.parent = null;
                break;
            }
        }

        return spawnGot;
    }

    public void ReturnMins(MinLight minLight)
    {
        minLight.gameObject.SetActive(false);
        minLight.transform.parent = MinPoolRoot;
        minLight.transform.localPosition = Vector3.zero;
    }

    public void ReturnSpawn(MinSpawnLight minSpawn)
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
