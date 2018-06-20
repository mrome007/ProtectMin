using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemyMovement : EnemyMovement
{
    [SerializeField]
    private Transform orangeEnemyObject;

    private Vector3 orangeRotateDirection;

    protected override void OnEnable()
    {
        speed = Random.Range(3f, 5f);
        maxSpeed = speed + (2 * speed);
        acceleration = Random.Range(10f, 15f);

        movementDirection = LeftDirection ? Vector3.left : Vector3.right;
        orangeRotateDirection = LeftDirection ? Vector3.forward : Vector3.back;
    }

    protected override void Update()
    {
        speed = GetSpeed(speed, maxSpeed, acceleration);
        transform.Translate(movementDirection * speed * Time.deltaTime);
        orangeEnemyObject.transform.Rotate(orangeRotateDirection* speed * 100f * Time.deltaTime);
    }
}
