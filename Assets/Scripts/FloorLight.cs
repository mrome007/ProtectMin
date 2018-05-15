using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLight : MinLight
{
    public FloorMin FloorMinReference { get; set; }

    public override void Initialize(Min minReference)
    {
        FloorMinReference = minReference as FloorMin;
    }

    public override void ReturnToPool(MinLight minLight)
    {
        FloorMinReference.MinPool.ReturnMins(this);
    }
}
