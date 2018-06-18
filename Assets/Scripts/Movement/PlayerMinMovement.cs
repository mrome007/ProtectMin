using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinMovement : MonoBehaviour 
{
    public float PlayerSpeed { get { return movementSpeed; } }

    public float MinsDistance { get { return distanceFromMin; } }

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashDuration;

    [SerializeField]
    private float distanceFromMin;

    [SerializeField]
    private Vector2 xAxisBoundary;

    [SerializeField]
    private Vector2 zAxisBoundary;

    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    private Vector3 movementVector;

    private bool isDashing;
    private Coroutine playerDashRoutine = null;
    private float dashMultiplier = 1f;

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

        movementVector.x *= dashMultiplier;
        if(Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            playerDashRoutine = StartCoroutine(PlayerDash(movementVector.x));
        }

        ChangePlayerDirection(movementVector.x < 0f);
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);

        //Clamp position;
        movementVector = transform.position;
        movementVector.x = Mathf.Clamp(movementVector.x, xAxisBoundary.x, xAxisBoundary.y);
        movementVector.z = Mathf.Clamp(movementVector.z, zAxisBoundary.x, zAxisBoundary.y);

        transform.position = movementVector;
    }

    private IEnumerator PlayerDash(float movement)
    {
        if(movement != 0f)
        {
            isDashing = true;
            dashMultiplier = dashSpeed;
            gameObject.layer = 13;

            yield return new WaitForSeconds(dashDuration);

            gameObject.layer = 0;
            dashMultiplier = 1f;
            isDashing = false;
        }
    }

    private void ChangePlayerDirection(bool flip)
    {
        playerSpriteRenderer.flipX = flip;
    }
}

