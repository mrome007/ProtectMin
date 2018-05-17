using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLight : MinLight
{
    public BlockMin BlockMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return BlockMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        BlockMinReference = minReference as BlockMin;
    }

    public override void ReturnToPool(MinLight minLight)
    {
        BlockMinReference.MinPool.ReturnMins(this);
    }
}
