using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeployState : IDeployState
{
    public IDeployState NextState { get; private set; }
    private float deployRange;

    public ProjectileDeployState()
    {
        NextState = null;
        deployRange = 5f;
    }

    public void SetNextState(IDeployState next)
    {
        NextState = next;
    }

    public void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins)
    {
        if(Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentPos.z - Camera.main.transform.position.z));
            worldPos.x = Mathf.Clamp(worldPos.x, currentPos.x - deployRange, currentPos.x + deployRange);
            if(mins[MinsType.Projectile].Count > 0)
            {
                var minLight = mins[MinsType.Projectile].Pop();

                var movement = minLight.GetComponent<MinMovement>();
                movement.SetMovementType(MinMovement.MinMovementType.Deploy);

                minLight.transform.position = worldPos;
            }
        }
    }
}
