using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMovement : MonoBehaviour 
{
    protected MinLight minLight;
    protected float minFlockSpeed;
    protected float currentDistance;
    protected Vector3 directionVector;
    protected Vector3? randomPosContainer;
    protected Vector3 randomPosition;

    public enum MinMovementType
    {
        None,
        Flock,
        Deploy
    }

    protected MinMovementType movementType;

    
    protected virtual void OnEnable()
    {
        if(minLight == null)
        {
            minLight = GetComponent<MinLight>();
        }

        randomPosContainer = null;
        randomPosition = Vector3.zero;
    }

    protected virtual void Update()
    {
        MoveMin();
    }

    protected virtual void MoveMinInFlock()
    {
        directionVector = minLight.BaseMin.MinPlayer.transform.position - transform.position;
        currentDistance = directionVector.sqrMagnitude;
        if(currentDistance >= Mathf.Pow(minLight.BaseMin.MinPlayer.MinsDistance, 2f))
        {
            minFlockSpeed += Time.deltaTime;
            transform.Translate(directionVector.normalized * minFlockSpeed * Time.deltaTime);
            randomPosContainer = null;
        }
        else
        {
            minFlockSpeed = minLight.BaseMin.MinPlayer.PlayerSpeed;
            if(!randomPosContainer.HasValue)
            {
                randomPosition.x = minLight.BaseMin.MinPlayer.transform.position.x + Random.Range(-3f, 3f);
                randomPosition.z = minLight.BaseMin.MinPlayer.transform.position.z + Random.Range(-1f, 1f);
                randomPosContainer = randomPosition;
            }

            directionVector = randomPosContainer.Value - transform.position;
            transform.Translate(directionVector.normalized * minFlockSpeed * Time.deltaTime);
            currentDistance = directionVector.sqrMagnitude;
            if(currentDistance < 1)
            {
                randomPosContainer = null;
            }
        }
    }

    protected virtual void MoveMinInDeploy()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);
        if(transform.position.x < -10f)
        {
            minLight.ReturnToPool();
        }
    }

    protected virtual void MoveMin()
    {
        switch(movementType)
        {
            case MinMovementType.Deploy:
                MoveMinInDeploy();
                break;

            case MinMovementType.Flock:
                MoveMinInFlock();
                break;

            default:
                break;
        }
    }

    public void SetMovementType(MinMovementType movement)
    {
        movementType = movement;
    }
}
