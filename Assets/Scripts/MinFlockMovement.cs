using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinFlockMovement : MonoBehaviour 
{
    private MinLight minLight;
    private float minSpeed;
    private float currentDistance;
    private Vector3 directionVector;
    private Vector3? randomPos;
    
    private void OnEnable()
    {
        if(minLight == null)
        {
            minLight = GetComponent<MinLight>();
        }
    }

    private void Update()
    {
        MoveMin();
    }

    private void MoveMin()
    {
        directionVector = minLight.BaseMin.MinPlayer.transform.position - transform.position;
        currentDistance = directionVector.sqrMagnitude;
        if(currentDistance >= Mathf.Pow(minLight.BaseMin.MinPlayer.MinsDistance, 2f))
        {
            minSpeed += 3.0f;
            transform.Translate(directionVector.normalized * minSpeed * Time.deltaTime);
            randomPos = null;
        }
        else
        {
            minSpeed = minLight.BaseMin.MinPlayer.PlayerSpeed;
            if(!randomPos.HasValue)
            {
                randomPos = new Vector3(minLight.BaseMin.MinPlayer.transform.position.x + Random.Range(-3f, 3f), 0f, minLight.BaseMin.MinPlayer.transform.position.z + Random.Range(-1f, 1f));
            }

            directionVector = randomPos.Value - transform.position;
            transform.Translate(directionVector.normalized * minSpeed * Time.deltaTime);
            currentDistance = directionVector.sqrMagnitude;
            if(currentDistance < 1)
            {
                randomPos = null;
            }
        }
    }
}
