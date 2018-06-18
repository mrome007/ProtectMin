using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLight : MinLight
{
    public FloorMin FloorMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return FloorMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        FloorMinReference = minReference as FloorMin;
    }
}
