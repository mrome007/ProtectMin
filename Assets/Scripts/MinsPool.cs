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
    private int NumberOfObjectsPerPool = 10;

    [SerializeField]
    private Transform MinPoolRoot;

    private Dictionary<MinsType, List<MinLight>> MinsPoolContainer;

    private void Awake()
    {
        MinsPoolContainer = new Dictionary<MinsType, List<MinLight>>();
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
                for(int count = 0; count < NumberOfObjectsPerPool; count++)
                {
                    var minLight = (MinLight)Instantiate(MinsLightObjects[(int)min.MinType]);
                    minLight.transform.parent = MinPoolRoot;
                    minLight.transform.localPosition = Vector3.zero;
                    minLight.Initialize(min);
                    minLight.gameObject.SetActive(false);
                    MinsPoolContainer[min.MinType].Add(minLight);
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

    public void ReturnMins(MinLight minLight)
    {
        minLight.gameObject.SetActive(false);
        minLight.transform.parent = MinPoolRoot;
        minLight.transform.localPosition = Vector3.zero;
    }

}

public enum MinsType
{
    None = 4,
    Block = 0,
    Floor = 1,
    Jump = 2,
    Projectile = 3
}
