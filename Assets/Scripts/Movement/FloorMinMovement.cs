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
            minLight.ChangeMinLightSpriteDirection(direction.x < 0f);
            var distance = direction.sqrMagnitude;
            var floorMin = minLight.BaseMin as FloorMin;
            var speed = floorMin.BuildSpeed;
            if(distance > 1f)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }
            else
            {
                targetPosition = null;
            }
        }
        else
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
            if(transform.position.x < -10f)
            {
                minLight.ReturnToPool();
            }
        }
    }

    public void SetFloorTarget(Vector3 target)
    {
        targetPosition = target;
        var floorMin = minLight.BaseMin as FloorMin;
        floorMin.BuildSpeed = (targetPosition.Value - transform.position).sqrMagnitude / 1f;
    }
}
