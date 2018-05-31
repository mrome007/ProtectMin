using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMinMovement : MinMovement 
{
    private Vector3? targetPosition;

    protected override void OnEnable()
    {
        base.OnEnable();

        targetPosition = null;
    }

    protected override void MoveMinInDeploy()
    {
        if(targetPosition.HasValue)
        {
            var direction = targetPosition.Value - transform.position;
            var distance = direction.sqrMagnitude;
            var floorMin = minLight.BaseMin as FloorMin;
            var speed = floorMin.BuildSpeed;
            if(distance > 1f)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }
        }
    }

    public void SetFloorTarget(Vector3 target)
    {
        targetPosition = target;
    }
}
