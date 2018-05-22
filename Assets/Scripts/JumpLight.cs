using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLight : MinLight
{
    public JumpMin JumpMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return JumpMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        JumpMinReference = minReference as JumpMin;
    }

    public override void ReturnToPool()
    {
        JumpMinReference.MinPool.ReturnMins(this);
    }
}
