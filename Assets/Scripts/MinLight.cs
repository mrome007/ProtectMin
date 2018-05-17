using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinLight : MonoBehaviour 
{
    public abstract Min BaseMin { get; }
    public abstract void Initialize(Min minReference);
    public abstract void ReturnToPool(MinLight minLight);
}
