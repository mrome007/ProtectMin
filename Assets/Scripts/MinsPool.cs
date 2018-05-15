using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinsPool : MonoBehaviour 
{
    [SerializeField]
    private List<Min> MinsMainObjects;
    
    [SerializeField]
    private List<MinLight> MinsLightObjects;

    private Dictionary<MinsType, List<GameObject>> MinsPoolContainer;

	private void Start()
    {
      
    }
}

public enum MinsType
{
    None,
    Block,
    Floor,
    Jump,
    Projectile
}
