﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinCollision : MonoBehaviour 
{
    [SerializeField]
    private PlayerControlMin playerControlMin;

    private void OnTriggerEnter(Collider other)
    {
        var minLight = other.GetComponent<MinLight>();
        if(minLight != null)
        {
            playerControlMin.AddMinToControlledMins(minLight);
            minLight.transform.parent = null;

            other.enabled = false;
            var movement = other.GetComponent<MinMovement>();
            movement.SetMovementType(MinMovement.MinMovementType.Flock);
        }

        var minSpawnLight = other.GetComponent<MinSpawnLight>();
        if(minSpawnLight != null)
        {
            other.enabled = false;
            minSpawnLight.SpawnMins();
            minSpawnLight.ShowMinSpawn(false);
        }
    }
}
