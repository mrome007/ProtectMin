using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyMovement : MonoBehaviour 
{
    public bool LeftDirection { get; set; }

    private Vector3? randomTarget;
    private float yellowSpeed;
    private float yellowMaxSpeed;
    private float yellowAcceleration;
    private Vector3 movementDirection;
    private Vector3 stopPosition;

    private float shakeTimer = 0f;
    private float shakeTime = 1f;
    private bool up;

    private void OnEnable()
    {
        randomTarget = null;
        yellowSpeed = Random.Range(5f, 6f);
        yellowMaxSpeed = 20f * yellowSpeed;
        yellowAcceleration = Random.Range(10f, 11f);
        movementDirection = Vector3.zero;
        stopPosition = transform.position;
        up = true;
    }

    private void Update()
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
            yellowSpeed = GetSpeed(yellowSpeed, yellowMaxSpeed, yellowAcceleration);
            var direction = randomTarget.Value - transform.position;
            transform.Translate(direction.normalized * yellowSpeed * Time.deltaTime);

            var sqrDistance = direction.sqrMagnitude;
            if(sqrDistance <= 1f)
            {
                randomTarget = null;
                up = !up;
                stopPosition = transform.position;
            }
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
