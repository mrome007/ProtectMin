using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinMovement : MonoBehaviour 
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Vector2 xAxisBoundary;

    [SerializeField]
    private Vector2 zAxisBoundary;

    private Vector3 movementVector;

    private void Awake()
    {
        movementVector = Vector3.zero;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        transform.Translate(movementVector * movementSpeed * Time.deltaTime);

        //Clamp position;
        movementVector = transform.position;
        movementVector.x = Mathf.Clamp(movementVector.x, xAxisBoundary.x, xAxisBoundary.y);
        movementVector.z = Mathf.Clamp(movementVector.z, zAxisBoundary.x, zAxisBoundary.y);

        transform.position = movementVector;
    }
}
