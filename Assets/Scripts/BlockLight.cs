using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLight : MinLight
{
    public BlockMin BlockMinReference { get; set; }

    public override void Initialize(Min minReference)
    {
        BlockMinReference = minReference as BlockMin;
    }
}
