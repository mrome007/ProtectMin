using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatLight : MinLight
{
    public FloatMin FloatMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return FloatMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        FloatMinReference = minReference as FloatMin;
    }
}
