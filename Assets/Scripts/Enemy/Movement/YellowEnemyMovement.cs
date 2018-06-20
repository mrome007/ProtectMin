using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyMovement : EnemyMovement
{
    private Vector3? randomTarget;
    private Vector3 stopPosition;

    private float shakeTimer = 0f;
    private float shakeTime = 1f;
    private bool up;

    protected override void OnEnable()
    {
        randomTarget = null;
        speed = Random.Range(5f, 6f);
        maxSpeed = 20f * speed;
        acceleration = Random.Range(10f, 11f);
        movementDirection = Vector3.zero;
        stopPosition = transform.position;
        up = true;
    }

    protected override void Update()
    {
        if(randomTarget == null)
        {
            movementDirection.x = Random.Range(-0.25f, 0.25f) + stopPosition.x;
            movementDirection.y = Random.Range(-0.25f, 0.25f) + stopPosition.y; 
            transform.position = movementDirection;
            shakeTimer += Time.deltaTime;
            if(shakeTimer >= shakeTime)
            {
                shakeTimer = 0f;
                movementDirection = LeftDirection ? Vector3.left : Vector3.right;
                movementDirection.y = up ? 10f : 0f;
                movementDirection.x *= (stopPosition.x + Random.Range(1f, 2f));
                randomTarget = movementDirection;
            }
        }
        else
        {
            speed = GetSpeed(speed, maxSpeed, acceleration);
            var direction = randomTarget.Value - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            var sqrDistance = direction.sqrMagnitude;
            if(sqrDistance <= 1f)
            {
                randomTarget = null;
                up = !up;
                stopPosition = transform.position;
            }
        }
    }
}
