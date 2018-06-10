using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinLight : MonoBehaviour 
{
    public abstract Min BaseMin { get; }
    public abstract void Initialize(Min minReference);

    [SerializeField]
    private SpriteRenderer minLightSpriteRenderer;

    public virtual void UpdateMinLightSpriteOrder(int order)
    {
        minLightSpriteRenderer.sortingOrder = order;
    }

    public virtual void ChangeMinLightSpriteDirection(bool flip)
    {
        minLightSpriteRenderer.flipX = flip;
    }

    public virtual void ReturnToPool()
    {
        UpdateMinLightSpriteOrder(0);
        BaseMin.MinPool.ReturnMins(this);
    }
}
