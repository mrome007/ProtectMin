using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLight : MinLight
{
    public JumpMin JumpMinReference { get; set; }

    public override void Initialize(Min minReference)
    {
        JumpMinReference = minReference as JumpMin;
    }
}
