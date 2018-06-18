using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstLight : MinLight
{
    public BurstMin BurstMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return BurstMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        BurstMinReference = minReference as BurstMin;
    }
}
