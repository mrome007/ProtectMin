using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinFlockMovement : MonoBehaviour 
{
    private MinLight minLight;
    private float minSpeed;
    private float currentDistance;
    private Vector3 directionVector;
    
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
        minSpeed = minLight.BaseMin.MinPlayer.PlayerSpeed;

        directionVector = minLight.BaseMin.MinPlayer.transform.position - transform.position;
        currentDistance = directionVector.sqrMagnitude;
        if(currentDistance > Mathf.Pow(minLight.BaseMin.MinPlayer.MinsDistance, 2f))
        {
            minSpeed++;
            transform.Translate(directionVector.normalized * minSpeed * Time.deltaTime);
        }
        else
        {

        }
    }
}
