using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyMovement : EnemyMovement
{
    private float maxHeight = 2f;
    private float minHeight = 0f;

    protected override void OnEnable()
    {
        speed = Random.Range(5f, 6f);
        maxSpeed = 2f * speed;
        acceleration = Random.Range(10f, 15f);
        movementDirection = new Vector3(LeftDirection ? -1f : 1f, 1f, 0f);
    }

    protected override void Update()
    {
        speed = GetSpeed(speed, maxSpeed, acceleration);
        transform.Translate(movementDirection * speed * Time.deltaTime);

        if(transform.position.y >= maxHeight)
        {
            movementDirection.y = -1f;
        }

        if(transform.position.y <= minHeight)
        {
            maxHeight = Random.Range(1f, 3f);
            movementDirection.y = 1f;
        }
    }
}
