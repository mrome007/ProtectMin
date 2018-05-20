using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinCollision : MonoBehaviour 
{
    [SerializeField]
    private PlayerControlMin playerControlMin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var minLight = other.GetComponent<MinLight>();
        if(minLight != null)
        {
            playerControlMin.AddMinToControlledMins(minLight);
            minLight.transform.parent = null;
        }
    }
}
