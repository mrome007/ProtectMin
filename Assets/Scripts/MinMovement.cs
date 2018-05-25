using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMovement : MonoBehaviour 
{
    private MinLight minLight;
    private float minFlockSpeed;
    private float currentDistance;
    private Vector3 directionVector;
    private Vector3? randomPos;

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
            randomPos = null;
        }
        else
        {
            minFlockSpeed = minLight.BaseMin.MinPlayer.PlayerSpeed;
            if(!randomPos.HasValue)
            {
                randomPos = new Vector3(minLight.BaseMin.MinPlayer.transform.position.x + Random.Range(-3f, 3f), 
                                        0f, 
                                        minLight.BaseMin.MinPlayer.transform.position.z + Random.Range(-1f, 1f));
            }

            directionVector = randomPos.Value - transform.position;
            transform.Translate(directionVector.normalized * minFlockSpeed * Time.deltaTime);
            currentDistance = directionVector.sqrMagnitude;
            if(currentDistance < 1)
            {
                randomPos = null;
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
