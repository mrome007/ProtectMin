using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
    public virtual bool LeftDirection { get; set; }

    protected float speed;
    protected float maxSpeed;
    protected float acceleration;
    protected Vector3 movementDirection;

    protected virtual void OnEnable()
    {
    }

    protected virtual void Update()
    {
    }

    protected float GetSpeed(float speed, float maxSpeed, float acceleration)
    {
        if(speed >= maxSpeed)
        {
            return speed;
        }

        speed = speed + acceleration * Time.deltaTime;

        return speed;
    }
}
