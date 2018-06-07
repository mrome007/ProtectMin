using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstMinMovement : MinMovement
{
    private Vector3? target;
    private Vector3 randomDirection;

    protected override void OnEnable()
    {
        base.OnEnable();

        target = null;
        randomDirection = Vector3.zero;
    }

    protected override void MoveMinInDeploy()
    {
        var burstMin = minLight.BaseMin as BurstMin;

        if(target.HasValue)
        {
            var direction = target.Value - transform.position;
            transform.Translate(direction.normalized * burstMin.BurstApproachSpeed * Time.deltaTime);
            if(direction.sqrMagnitude < 1f)
            {
                target = null;
                randomDirection.x = (float)Random.Range(-1f, 1f);
                randomDirection.y = 1f;
            }
        }
        else
        {
            transform.Translate(randomDirection.normalized * burstMin.BurstSpeed * Time.deltaTime);
            if((transform.position.x < -10f || transform.position.x > 10f) || (transform.position.y < -2f || transform.position.y > 11f))
            {
                minLight.ReturnToPool();
            }
        }
    }

    public void SetBurstTarget(Vector3 targetPos)
    {
        target = targetPos;
    }
}
