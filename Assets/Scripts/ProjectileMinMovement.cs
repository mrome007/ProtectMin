using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMinMovement : MinMovement
{
    private Coroutine ExaggeratedFlightRoutine = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        if(ExaggeratedFlightRoutine != null)
        {
            StopCoroutine(ExaggeratedFlightRoutine);
            ExaggeratedFlightRoutine = null;
        }
        ExaggeratedFlightRoutine = null;
    }

    protected override void MoveMinInDeploy()
    {
        if(ExaggeratedFlightRoutine == null)
        {
            ExaggeratedFlightRoutine = StartCoroutine(ExaggeratedFlight());
        }
        
        if((transform.position.x < -10f || transform.position.x > 10f) || (transform.position.y < -2f || transform.position.y > 11f))
        {
            if(ExaggeratedFlightRoutine != null)
            {
                StopCoroutine(ExaggeratedFlightRoutine);
                ExaggeratedFlightRoutine = null;
            }
            minLight.ReturnToPool();
        }
    }

    private IEnumerator ExaggeratedFlight()
    {
        var timer = 0f;
        var initialPos = transform.position;
        var projectileMin = minLight.BaseMin as ProjectileMin;
        var launchDuration = projectileMin.LaunchDuration;
        var flightDuration = projectileMin.FlightDuration;
        var projectileSpeed = projectileMin.ProjectileSpeed;
        var projectileAcceleration = projectileMin.ProjectileAcceleration;

        while(timer < launchDuration)
        {
            randomPosition.x = initialPos.x + Random.Range(-0.1f, 0.1f);
            randomPosition.y = initialPos.y + Random.Range(-0.1f, 0.1f);
            randomPosition.z = initialPos.z + Random.Range(-0.1f, 0.1f);

            transform.position = randomPosition;

            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = initialPos;
        var directionNorm = (initialPos - minLight.BaseMin.MinPlayer.transform.position).normalized;
        timer = 0f;

        while(timer < flightDuration)
        {
            transform.Translate(directionNorm * projectileSpeed * Time.deltaTime);
            yield return null;
            timer += Time.deltaTime;
            projectileSpeed += projectileAcceleration;
        }
    }
}
