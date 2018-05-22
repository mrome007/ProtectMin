using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedMovement : MonoBehaviour 
{
    private MinLight minLight;

    private void Awake()
    {
        minLight = GetComponent<MinLight>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * 5f * Time.deltaTime);
        if(transform.position.x < -10f)
        {
            minLight.ReturnToPool();
        }
    }
}
