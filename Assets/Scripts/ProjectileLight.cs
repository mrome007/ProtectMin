using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLight : MinLight
{
    public ProjectileMin ProjectileMinReference { get; set; }
    public override Min BaseMin
    {
        get
        {
            return ProjectileMinReference;
        }
    }

    public override void Initialize(Min minReference)
    {
        ProjectileMinReference = minReference as ProjectileMin;
    }

    public override void ReturnToPool(MinLight minLight)
    {
        ProjectileMinReference.MinPool.ReturnMins(this);
    }
}
