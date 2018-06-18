using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDeployState : IDeployState
{
    public IDeployState NextState { get; private set; }
    private Vector3 targetPosition;
    private float xOffset;
    private float yOffset;

    public FloorDeployState()
    {
        NextState = null;
        targetPosition = Vector3.zero;
        xOffset = 0.5f;
        yOffset = 1f;
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
            targetPosition = worldPos;
            var buildCount = 0;

            if(mins[MinsType.Floor].Count <= 0)
            {
                return;
            }

            while(mins[MinsType.Floor].Count > 0)
            {
                var minLight = mins[MinsType.Floor].Pop();

                var movement = minLight.GetComponent<MinMovement>();
                (movement as FloorMinMovement).SetFloorTarget(targetPosition);
                movement.SetMovementType(MinMovement.MinMovementType.Deploy);

                buildCount++;
                if(buildCount % 3 == 0)
                {
                    targetPosition = worldPos;
                    var multiplier = buildCount / 3;
                    targetPosition.x += multiplier * xOffset;
                }
                else
                {
                    targetPosition.y += yOffset;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            targetPosition = Vector3.zero;
        }
    }
}
