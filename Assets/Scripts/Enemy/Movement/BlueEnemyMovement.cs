using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyMovement : MonoBehaviour 
{
    public bool LeftDirection { get; set; }

    private float blueHopSpeed;
    private float blueMaxSpeed;
    private float blueAcceleration;
    private Vector3 blueMovementDirection;

    private float maxHeight = 2f;
    private float minHeight = 0f;

    private void OnEnable()
    {
        blueHopSpeed = Random.Range(5f, 6f);
        blueMaxSpeed = 2f * blueHopSpeed;
        blueAcceleration = Random.Range(10f, 15f);
        blueMovementDirection = new Vector3(LeftDirection ? -1f : 1f, 1f, 0f);

    }

    private void Update()
    {
        blueHopSpeed = GetSpeed(blueHopSpeed, blueMaxSpeed, blueAcceleration);
        transform.Translate(blueMovementDirection * blueHopSpeed * Time.deltaTime);

        if(transform.position.y >= maxHeight)
        {
            blueMovementDirection.y = -1f;
        }

        if(transform.position.y <= minHeight)
        {
            maxHeight = Random.Range(1f, 3f);
            blueMovementDirection.y = 1f;
        }
    }

    private float GetSpeed(float speed, float maxSpeed, float acceleration)
    {
        if(speed >= maxSpeed)
        {
            return speed;
        }

        speed = speed + acceleration * Time.deltaTime;

        return speed;
    }
}
