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

    private bool tapStart = false;
    private char key;
    private float tapTimer = 0f;
    private float tapTime = 0.5f;
    private float dashDirection = 1f;

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

        movementVector.x = isDashing ? dashDirection : movementVector.x;
        movementVector.x *= dashMultiplier;

        DashInput();

        ChangePlayerDirection(movementVector.x < 0f);
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);

        //Clamp position;
        movementVector = transform.position;
        movementVector.x = Mathf.Clamp(movementVector.x, xAxisBoundary.x, xAxisBoundary.y);
        movementVector.z = Mathf.Clamp(movementVector.z, zAxisBoundary.x, zAxisBoundary.y);

        transform.position = movementVector;

        if(tapStart)
        {
            tapTimer += Time.deltaTime;
            if(tapTimer > tapTime)
            {
                tapTimer = 0f;
                tapStart = false;
                key = ' ';
            }
        }
    }

    private IEnumerator PlayerDash(float movement)
    {
        if(movement != 0f)
        {
            dashDirection = movement;
            isDashing = true;
            dashMultiplier = dashSpeed;
            gameObject.layer = 13;

            var timer = 0f;
            var scale = transform.localScale;
            while(timer < dashDuration / 2f)
            {
                scale.x += 0.1f;
                scale.y += 0.1f;
                transform.localScale = scale;
                timer += Time.deltaTime;
                yield return null;
            }

            while(timer < dashDuration)
            {
                scale.x -= 0.1f;
                scale.y -= 0.1f;

                scale.x = Mathf.Clamp(scale.x, 1f, 5f);
                scale.y = Mathf.Clamp(scale.y, 1f, 5f);

                transform.localScale = scale;
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one;
            gameObject.layer = 0;
            dashMultiplier = 1f;
            isDashing = false;
        }
    }

    private void ChangePlayerDirection(bool flip)
    {
        playerSpriteRenderer.flipX = flip;
    }

    private void DashInput()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(tapStart && !isDashing && key == 'd') 
            {
                playerDashRoutine = StartCoroutine(PlayerDash(1f));
                tapStart = false;
                tapTimer = 0f;
                key = ' ';
            }
            else
            {
                tapStart = true;
                key = 'd';
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(tapStart && !isDashing && key == 'a')
            {
                playerDashRoutine = StartCoroutine(PlayerDash(-1f));
                tapStart = false;
                tapTimer = 0f;
                key = ' ';
            }
            else
            {
                tapStart = true;
                key = 'a';
            }
        }
    }
}

