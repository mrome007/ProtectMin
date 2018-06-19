using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemyMovement : MonoBehaviour 
{
    public bool LeftDirection { get; set; }

    [SerializeField]
    private Transform orangeEnemyObject;

    private float orangeSpeed;
    private float orangeMaxSpeed;
    private float orangeAcceleration;
    private Vector3 orangeDirection;
    private Vector3 orangeRotateDirection;

    private void OnEnable()
    {
        orangeSpeed = Random.Range(3f, 5f);
        orangeMaxSpeed = orangeSpeed + (2 * orangeSpeed);
        orangeAcceleration = Random.Range(10f, 15f);

        orangeDirection = LeftDirection ? Vector3.left : Vector3.right;
        orangeRotateDirection = LeftDirection ? Vector3.forward : Vector3.back;
    }

    private void Update()
    {
        orangeSpeed = GetOrangeSpeed(orangeSpeed, orangeMaxSpeed, orangeAcceleration);
        transform.Translate(orangeDirection * orangeSpeed * Time.deltaTime);
        orangeEnemyObject.transform.Rotate(orangeRotateDirection* orangeSpeed * 100f * Time.deltaTime);
    }

    private float GetOrangeSpeed(float speed, float maxSpeed, float acceleration)
    {
        if(speed >= maxSpeed)
        {
            return speed;
        }

        speed = speed + acceleration * Time.deltaTime;

        return speed;
    }
}
